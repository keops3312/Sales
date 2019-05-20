using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class AddProductViewModel:BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes

        private string description; 
        private string price ; 
        private string remarks; 
        private bool isRunning; 
        private bool isEnabled;

        private ImageSource imageSource;

        private MediaFile file;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.SetValue(ref this.isEnabled, value);
            }
        }


        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                SetValue(ref this.description, value);
            }
        }


        public string Remarks
        {
            get
            {
                return this.remarks;
            }
            set
            {
                SetValue(ref this.remarks, value);
            }
        }

        public string Price
        {
            get
            {
                return this.price;
            }
            set
            {
                SetValue(ref this.price, value);
            }
        }


        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set
            {
                SetValue(ref this.imageSource, value);
            }
        }

      

        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "noImage";
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }


        #endregion

        #region Methods
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.description))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       Languages.DescriptionError,
                                       Languages.Accept);


                return;
            }

            var price = decimal.Parse(this.price);
            if (price<0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       Languages.PriceError,
                                       Languages.Accept);


                return;
            }



            if (string.IsNullOrEmpty(this.remarks))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       "Please Insert a Remarks",
                                       Languages.Accept);


                return;
            }

            /*check Connection*/
            this.isRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

               
                this.isRunning = false;
                this.isEnabled = true;
                return;

            }
          
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();


            /*para la camara*/

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
                ImageArray = imageArray,
                PublishOn=DateTime.Now,
            };



            var response = await this.apiService.Post(
                url, /*"http://192.168.1.79:16094*/
               prefix,/*/api*/
               controller,product);/*Products*/




            if (!response.IsSucces)
            {
                this.isRunning = false;
                this.isEnabled = true;
                
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;

            }


            var newProduct = (Product)response.Result; /*locasteamos*/
            var viewModel = ProductsViewModel.GetInstance();/*de esta manera se actualiza la lista de productos cuandose agraga uno nuevo*/
            viewModel.Products.Add(newProduct);/*y se realiza para no volver ha realizar un llamado al server*/
            

            this.isRunning = false;
            this.isEnabled = true;

            await Application.Current.MainPage.Navigation.PopAsync();/*para desapilar */



        }



        /*Para tomar las fotos*/

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }


        #endregion

    }
}
