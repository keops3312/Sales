using GalaSoft.MvvmLight.Command;
using Sales.Helpers;
using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.Views
{
    public class MenuItemViewModel
    {

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

        #endregion


        #region Commands
        public ICommand GotoCommand
        {

            get
            {

                return new RelayCommand(Goto);
            }
        }


        #endregion


        #region Methods
        private void Goto()
        {
            if (this.PageName == "LoginPage")
            {
                Settings.AccesToken = string.Empty;
                Settings.TokenType = string.Empty;
                Settings.IsRemembered = false;

                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage=new NavigationPage(new LoginPage());
            }
        }
        #endregion


    }
}
