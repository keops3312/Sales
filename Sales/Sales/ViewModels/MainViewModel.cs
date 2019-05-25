

namespace Sales.ViewModels
{

    using GalaSoft.MvvmLight.Command;
    using Sales.Helpers;
    using Sales.Views;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MainViewModel:INotifyPropertyChanged
    {

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region ViewModels

        public LoginViewModel Login
        {
            get;
            set;

        }
        public EditProductViewModel EditProduct
        {
            get;
            set;

        }
        public ProductsViewModel Products
        {
            get; set;
        }

        public AddProductViewModel AddProduct
        {
            get; set;
        }

        public RegisterViewModel Register
        {
            get;set;
        }


        #endregion

        #region Constructor
        public MainViewModel()
        {

            instance = this;
            this.LoadMenu();
           
          // this.Products = new ProductsViewModel();
            
        }
        #endregion

        #region Clase singleton para cambiar de viewmodels

        private static MainViewModel instance;

       

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
            //await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
            await App.Navigator.PushAsync(new AddProductPage());
        }



        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });
        }

        #endregion


    }
}
