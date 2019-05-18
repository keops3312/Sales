
namespace Sales.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        /*Aqui van las traducciones*/
        public static string Error
        {
            get { return Resource.Error; }
        }


        public static string Accept
        {
            get { return Resource.Accept; }
        }


        public static string CheckInternet
        {
            get { return Resource.CheckInternet; }
        }


        public static string Products
        {
            get { return Resource.Products; }
        }

        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }


        public static string AddProducts
        {
            get { return Resource.AddProducts; }
        }

        public static string Description
        {
            get { return Resource.Description; }
        }


        public static string DescriptionPlaceHolder
        {
            get { return Resource.DescriptionPlaceHolder; }
        }

        public static string PricePlaceHolder
        {
            get { return Resource.PricePlaceHolder; }
        }


        public static string Price
        {
            get { return Resource.Price; }
        }


        public static string RemarksPlaceHolder
        {
            get { return Resource.RemarksPlaceHolder; }
        }

        public static string Remarks
        {
            get { return Resource.Remarks; }
        }

        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }

        public static string Save
        {
            get { return Resource.Save; }
        }

        public static string DescriptionError
        {
            get { return Resource.DescriptionError; }
        }


        public static string PriceError
        {
            get { return Resource.PriceError; }
        }




    }

}
