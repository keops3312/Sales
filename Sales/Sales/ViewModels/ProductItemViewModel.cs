﻿

namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using Sales.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductItemViewModel:Product
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Commands
        public ICommand DeleteProductCommand
        {
            get
            {

                return new RelayCommand(DeleteProduct);
            }
           


        }


        public ICommand EditProductCommand
        {

            get
            {
                return new RelayCommand(EditProduct);
            }
        }




        #endregion

        #region Constructors
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Methods

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            //await Application.Current.MainPage.Navigation.PushAsync(new EditProductPage());
            await App.Navigator.PushAsync(new EditProductPage());
        }

        private async void DeleteProduct()
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

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

               
                return;

            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.Delete(
                url, /*"http://192.168.1.79:16094*/
               prefix,/*/api*/
               controller, this.ProductId, Settings.TokenType, Settings.AccesToken);/*Products*/


            if (!response.IsSucces)
            {
                
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;

            }



            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProduct = productsViewModel.Products.Where(p => p.ProductId == this.ProductId).FirstOrDefault();

            if (deleteProduct != null)
            {
                productsViewModel.Products.Remove(deleteProduct);

            }



        } 
        #endregion
    }
}
