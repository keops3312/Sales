using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        //Intanciamos las ViewModel de cada Page
        public ProductsViewModel Products { get; set; }
        //instanciamos la view model dentro del constructor
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }

    }
}
