using Sales.Common.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

            var response = await this.apiService.GetList<Product>(
                "http://localhost:16094",
                "/api",
                "/Products");


            if (!response.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", response.Message, "Accept");
                return;

            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }

        #endregion



    }
}
