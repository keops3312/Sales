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
    public class ProductsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region attributes
        /*private ObservableCollection<Product> products; originalmete para solo mostrar una lista sin funciones*/
        private bool isRefreshing;


        private ObservableCollection<ProductItemViewModel> products;
        #endregion

        #region Properties
        /*  public ObservableCollection<Product> Products
          {
              get
              {
                  return this.products;
              }
              set
              {
                  SetValue(ref this.products, value);
              }
          }*/
        public ObservableCollection<ProductItemViewModel> Products
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
            instance = this;//aqui le digo que la instancia es el form actual
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        #endregion

        #region Methods
        private async void LoadProducts()
        {
           this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

                await Application.Current.MainPage.Navigation.PopAsync();/*para desapilar */
                this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;

            }

            this.IsRefreshing = false;
            var list = (List<Product>)response.Result;

            var myList = list.Select(p => new ProductItemViewModel
            {

                Description=p.Description,
                ImageArray=p.ImageArray,
                ImagePath=p.ImagePath,
                IsAvailable=p.IsAvailable,
                Price=p.Price,
                ProductId=p.ProductId,
                PublishOn=p.PublishOn,
                Remarks=p.Remarks,

            });

            /*para funciones ne lista*/
            //var myList = new List<ProductItemViewModel>();
            //foreach (var item in list)
            //{
            //    myList.Add(new ProductItemViewModel {




            //    });
            //}
            /**/
            this.Products = new ObservableCollection<ProductItemViewModel>(myList);
            this.IsRefreshing = false;

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

        #region Singleton (para llevar valores entre viewmodels)
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
