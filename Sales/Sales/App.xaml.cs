using Sales.Helpers;
using Sales.ViewModels;
using Sales.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sales
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }

        /*ventanas normales*/
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new ProductsPage();
        //}

        /*ventana de navegacion*/
        public App()
        {
            InitializeComponent();


            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.AccesToken))
            {

                MainViewModel.GetInstance().Products = new ProductsViewModel();
                MainPage =new MasterPage();/*new NavigationPage(new ProductsPage());*/
            }
            else
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());

            }
            ////MainPage = new NavigationPage(new ProductsPage());

            //MainViewModel.GetInstance().Login = new LoginViewModel();
            //MainPage = new NavigationPage(new LoginPage());
            ////MainPage = new LoginPage();
        }

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
