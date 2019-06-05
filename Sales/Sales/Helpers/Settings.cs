
/*esto es para guardar los valores asi como en el equipo*/
namespace Sales.Helpers
{

    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public class Settings
    {

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string tokenType = "TokenType";
        private const string accesToken = "AccesToken";
        private const string isRemembered = "IsRemembered";/*aqui le coloco el paramtreo a copiar para concatenar*/
        private static readonly string stringDefault = string.Empty;
        private static readonly bool booleanDefault = false;
        private const string userASP = "UserASP";

        #endregion


        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }

        public static string AccesToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(accesToken, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(accesToken, value);
            }
        }



        public static bool IsRemembered
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemembered, booleanDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemembered, value);
            }
        }



        public static string UserASP
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userASP, value);
            }
        }

    }
}
