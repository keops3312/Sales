

namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductItemViewModel:Product
    {



        private ApiService apiService;

        #region Commands
        public ICommand DeleteProductCommand
        {
            get
            {

                return new RelayCommand(DeleteProduct);
            }
           

        }


        #endregion

        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
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

                await Application.Current.MainPage.Navigation.PopAsync();/*para desapilar */
                return;

            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.Delete(
                url, /*"http://192.168.1.79:16094*/
               prefix,/*/api*/
               controller,this.ProductId);/*Products*/

            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProduct = productsViewModel.Products.Where(p => p.ProductId == this.ProductId).FirstOrDefault();

            if (deleteProduct != null)
            {
                productsViewModel.Products.Remove(deleteProduct);

            }



        }
    }
}
