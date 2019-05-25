﻿using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class EditProductViewModel:BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private Product product;

        private bool isRunning;

        private bool isEnabled;

        private ImageSource imageSource;

        private MediaFile file;
        #endregion

        #region Constructors
        public EditProductViewModel(Product product)
        {
            this.product = product;
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = product.ImageFullPath;
            
           
        }
        #endregion

        #region Properties
        public Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                SetValue(ref this.product, value);
            }
        }

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


        #region Commands

        public ICommand DeleteCommand
        {
            get
            {

                return new RelayCommand(Delete);
            }



        }
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
            if (string.IsNullOrEmpty(this.Product.Description))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       Languages.DescriptionError,
                                       Languages.Accept);


                return;
            }

            
            if (this.Product.Price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       Languages.PriceError,
                                       Languages.Accept);


                return;
            }



            if (string.IsNullOrEmpty(this.Product.Remarks))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       "Please Insert a Remarks",
                                       Languages.Accept);


                return;
            }

            /*check Connection*/
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);


                this.IsRunning = false;
                this.IsEnabled = true;
                return;

            }

          

            /*para la camara*/

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.Product.ImageArray = imageArray;
                
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();


            var response = await this.apiService.Put(
                url, 
               prefix,
               controller, this.Product,this.Product.ProductId, Settings.TokenType, Settings.AccesToken);



            if (!response.IsSucces)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    response.Message, 
                    Languages.Accept);
                return;

            }


            var newProduct = (Product)response.Result; /*locasteamos*/
            var productsViewModel = ProductsViewModel.GetInstance();/*de esta manera se actualiza la lista de productos cuandose agraga uno nuevo*/

            var oldProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault();

            if (oldProduct != null)
            {

                productsViewModel.MyProducts.Remove(oldProduct);
            }



            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();


            this.IsRunning = false;
            this.IsEnabled = true;

            // await Application.Current.MainPage.Navigation.PopAsync();/*para desapilar */
            await App.Navigator.PopAsync();


        }

        private async void Delete()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Delete,
                Languages.DeleteConfirmed,
                Languages.Yes,
                Languages.No);


            if (!answer)
            {

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

                this.IsRunning = false;
                this.IsEnabled = true;
                return;

            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.Delete(
                url, /*"http://192.168.1.79:16094*/
               prefix,/*/api*/
               controller, this.Product.ProductId, Settings.TokenType, Settings.AccesToken);/*Products*/


            if (!response.IsSucces)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);

                this.IsRunning = false;
                this.IsEnabled = true;
                return;

            }



            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault();

            if (deleteProduct != null)
            {
                productsViewModel.MyProducts.Remove(deleteProduct);

            }

            this.IsRunning = false;
            this.IsEnabled = true;
            productsViewModel.RefreshList();
            // await Application.Current.MainPage.Navigation.PopAsync();
            await App.Navigator.PopAsync();

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
