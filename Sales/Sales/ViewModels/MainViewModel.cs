

namespace Sales.ViewModels
{

    using GalaSoft.MvvmLight.Command;
    using Sales.Views;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MainViewModel:INotifyPropertyChanged
    {



        #region ViewModels
        public ProductsViewModel Products
        {
            get; set;
        }

        public AddProductViewModel AddProduct
        {
            get; set;
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
            
        }
        #endregion

        #region Clase singleton para cambiar de viewmodels

        private static MainViewModel instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public static MainViewModel GetInstance()
        {

            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }



        #endregion

        #region Commands
        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }

        }
        #endregion

        #region Methods
        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
        #endregion


    }
}
