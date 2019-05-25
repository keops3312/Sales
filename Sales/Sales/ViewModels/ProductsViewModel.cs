using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;

        private DataService dataService;
        #endregion

        #region attributes
        /*private ObservableCollection<Product> products; originalmete para solo mostrar una lista sin funciones*/
        private bool isRefreshing;

        private string filter;


        private ObservableCollection<ProductItemViewModel> products;
        #endregion

        #region Properties


     


        public List<Product> MyProducts { get; set; }/*to edit*/


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


        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                SetValue(ref this.filter, value);
            }
        }


        #endregion

        #region Constructor 
        public ProductsViewModel()
        {
            instance = this;//aqui le digo que la instancia es el form actual
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadProducts();
        }

        #endregion

        #region Methods
        private async void LoadProducts()
        {
           this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (connection.IsSucces)
            {

                var answer = await this.LoadProductsFromAPI();
                if (answer)
                {

                    this.SaveProductsToDB();
                }
            }
            else
            {
                await this.LoadProductsFromDB();
               

            }

            if(this.MyProducts==null || this.MyProducts.Count == 0)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                               Languages.NoProductsMessage,
                                                Languages.Accept);
                this.IsRefreshing = false;

                return;
            }


            this.RefreshList();
            this.IsRefreshing = false;
           
           
        }

        private async Task LoadProductsFromDB()
        {
            this.MyProducts = await this.dataService.GetAllProducts();
        }

        private async Task SaveProductsToDB()
        {
            await dataService.DeleteAllProducts();

            await dataService.Insert(this.MyProducts);
        }

        private async Task<bool> LoadProductsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.GetList<Product>(
              url, /*"http://192.168.1.79:16094*/
             prefix,/*/api*/
             controller, Settings.TokenType, Settings.AccesToken);/*Products y asi co  todos los demas servicios*/

            if (!response.IsSucces)
            {
                
                return false;

            }


            this.MyProducts = (List<Product>)response.Result;
            return true;


        }

        public void RefreshList()
        {
            var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
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

            /*para funciones ne lista*/
            //var myList = new List<ProductItemViewModel>();
            //foreach (var item in list)
            //{
            //    myList.Add(new ProductItemViewModel {




            //    });
            //}
            /**/
            this.Products = new ObservableCollection<ProductItemViewModel>(
                myListProductItemViewModel.OrderBy(p => p.Description));
        }


        private void SearchProduct()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {

                var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
                {

                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProductId = p.ProductId,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,

                }).Where(p => p.Description.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Products = new ObservableCollection<ProductItemViewModel>(
                myListProductItemViewModel.OrderBy(p => p.Description));
            }
            else
            {
                var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
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

                this.Products = new ObservableCollection<ProductItemViewModel>(
                myListProductItemViewModel.OrderBy(p => p.Description));
            }
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

        public ICommand SearchProductCommand
        {
            get
            {
                return new RelayCommand(SearchProduct);
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
