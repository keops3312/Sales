using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sales.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        #region ViewModels
        public ProductsViewModel Products
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


    }
}
