using Sales.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sales
{
    public partial class App : Application
    {
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

            MainPage = new NavigationPage(new ProductsPage());
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
