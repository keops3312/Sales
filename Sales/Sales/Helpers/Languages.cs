
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
    }

}
