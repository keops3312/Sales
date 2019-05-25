using GalaSoft.MvvmLight.Command;
using Sales.Helpers;
using Sales.Services;
using Sales.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {


        #region Services
        private ApiService apiService;

        #endregion

        #region attributes
        private string email;
        private string password;
        private bool isRemembered;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region properties
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }


        public bool IsRemembered
        {
            get
            {
                return this.isRemembered;
            }
            set
            {
                SetValue(ref this.isRemembered, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                SetValue(ref this.isEnabled, value);
            }
        }


        public string Email
        {
            get
            {
                return this.email;
            }
   
            set
            {
                SetValue(ref this.email, value);
            }
        }


        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                SetValue(ref this.password, value);
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {

            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRunning = false;
        }
        #endregion


        #region Commands
        public ICommand ForgotPasswordComand
        {
            get
            {
                return new RelayCommand(ForgotPassword);
            }
        }

        private void ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {


            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                       Languages.EmailValidation,
                                       Languages.Accept);

               
                return;


            }



            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                      Languages.PasswordValidation,
                                      Languages.Accept);


                return;
            }
            /*check conection*/

            this.IsRunning = true;
            this.IsEnabled = false;


            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                        connection.Message,
                                        Languages.Accept);

                this.IsRunning = false;
                this.IsEnabled = true;
                return;

            }
            /*EL SERVICIO*/

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var token = await this.apiService.GetToken(url, this.Email, this.password);

            if(token==null || string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                     Languages.SomethingWrong,
                                     Languages.Accept);


                return;
            }

            Settings.TokenType = token.TokenType;
            Settings.AccesToken = token.AccessToken;
            Settings.IsRemembered = this.IsRemembered;

            this.IsRunning = false;
            this.IsEnabled = true;
            MainViewModel.GetInstance().Products = new ProductsViewModel();
            Application.Current.MainPage = new MasterPage();//ProductsPage();
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
             }
        }

        private void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public ICommand LoginFacebookComand
        {
            get
            {
                return new RelayCommand(LoginFacebook);
            }
        }

        private void LoginFacebook()
        {
            throw new NotImplementedException();
        }

        public ICommand LoginTwitterComand
        {
            get
            {
                return new RelayCommand(LoginTwitter);
            }
        }

        private void LoginTwitter()
        {
            throw new NotImplementedException();
        }

        public ICommand LoginInstagramComand
        {
            get
            {
                return new RelayCommand(LoginInstagram);
            }
        }

        private void LoginInstagram()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Methods

        #endregion










    }
}
