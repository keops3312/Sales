using GalaSoft.MvvmLight.Command;
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

        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
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


            var product = new Product
            {
                Description=this.Description,
                Price=price,/*lo hicimos variable*/
                Remarks=this.Remarks,
                PublishOn=DateTime.Now,
                //falta fecha, isavaliable,la imagen

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

        #endregion

    }
}
