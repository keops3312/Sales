﻿using Newtonsoft.Json;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using Sales.ViewModels;
using Sales.Views;
using System;
using System.Threading.Tasks;
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

            var mainViewModel = MainViewModel.GetInstance();

            if (Settings.IsRemembered)
            {

                if (!string.IsNullOrEmpty(Settings.UserASP))
                {
                    mainViewModel.UserASP = JsonConvert.DeserializeObject<MyUserASP>(Settings.UserASP);
                }

                mainViewModel.Products = new ProductsViewModel();
                this.MainPage = new MasterPage();
            }
            else
            {
                mainViewModel.Login = new LoginViewModel();
                this.MainPage = new NavigationPage(new LoginPage());
            }

        }


         public static Action HideLoginView
        {
            get
            {
                return new Action(() => Current.MainPage = new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(TokenResponse token)
        {
            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            Settings.IsRemembered = true;
            Settings.AccesToken = token.AccessToken;
            Settings.TokenType = token.TokenType;

            var apiService = new ApiService();
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsersController"].ToString();
            var response = await apiService.GetUser(url, prefix, $"{controller}/GetUser", token.UserName, token.TokenType, token.AccessToken);
            if (response.IsSucces)
            {
                var userASP = (MyUserASP)response.Result;
                MainViewModel.GetInstance().UserASP = userASP;
                Settings.UserASP = JsonConvert.SerializeObject(userASP);
            }

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            Application.Current.MainPage = new MasterPage();
        }





        #region Methods

        //if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.AccesToken))
        //{

        //    MainViewModel.GetInstance().Products = new ProductsViewModel();
        //    MainPage =new MasterPage();/*new NavigationPage(new ProductsPage());*/
        //}
        //else
        //{
        //    MainViewModel.GetInstance().Login = new LoginViewModel();
        //    MainPage = new NavigationPage(new LoginPage());

        //}
        ////MainPage = new NavigationPage(new ProductsPage());

        //MainViewModel.GetInstance().Login = new LoginViewModel();
        //MainPage = new NavigationPage(new LoginPage());
        ////MainPage = new LoginPage();
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



