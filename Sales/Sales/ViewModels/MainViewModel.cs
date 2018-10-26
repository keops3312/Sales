

namespace Sales.ViewModels
{
    #region libraries (Librerias)
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Views; 
    using Xamarin.Forms;
    #endregion

    public class MainViewModel
    {

        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);

            }
        }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }

        //Intanciamos las ViewModel de cada Page
        public ProductsViewModel Products { get; set; }

        public AddProductViewModel AddProduct { get; set; }


        public EditProductViewModel EditProduct { get; set; }
        //instanciamos la view model dentro del constructor
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
            
        }


    }
}
