

using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        /// objetos en el xaml
        ///  IsRefreshing="{Binding IsRefreshing}" es un reloj de tiempo
        ///   RefreshCommand="{Binding RefreshCommand}" elemento para refresacar
        /// </summary>

        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;//Propiedad

        private ObservableCollection<ProductItemViewModel> products;
        #endregion

        #region Properties
        public ObservableCollection<ProductItemViewModel> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }


        public List<Product> myProducts { get; set; } 

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }//el objeto 
        #endregion

        #region Constructors
        public ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.GetList<Product>
                (url,
                 prefix,
                 controller);

            if (!response.IsSuccess)
            {

                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            this.myProducts = (List<Product>)response.Result;
            this.RefreshList();
           
        }

        public void RefreshList()
        {
            var mylistProductItemViewModel = myProducts.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                IsAvailable = p.IsAvailable,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishOn = p.PublishOn,
                Remarks = p.Remarks,
            });

            //var mylist =new List<ProductItemViewModel>();
            //foreach (var item in list)
            //{
            //    mylist.Add(new ProductItemViewModel
            //        {



            //        }

            //        );
            //}

            this.Products = new ObservableCollection<ProductItemViewModel>(
                mylistProductItemViewModel.OrderBy(p => p.Description));
            this.IsRefreshing = false;

        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
        #endregion


        #region Singleton (duvuelve la vista cargada en memoria)
        private static ProductsViewModel instance;
        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ProductsViewModel();
            }
            return instance;
        }
        #endregion
    }
}
