﻿using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region attributes
        private ObservableCollection<Product> products;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                SetValue(ref this.products, value);
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                SetValue(ref this.isRefreshing, value);
            }
        }

        #endregion

        #region Constructor 
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.isRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

                await Application.Current.MainPage.Navigation.PopAsync();/*para desapilar */
                this.isRefreshing = false;
                return;

            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.GetList<Product>(
                url, /*"http://192.168.1.79:16094*/
               prefix,/*/api*/
               controller);/*Products*/

           


            if (!response.IsSucces)
            {
                this.isRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;

            }

            this.isRefreshing = false;
            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
           
        }

        #endregion


        #region ICommands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
        #endregion





    }
}