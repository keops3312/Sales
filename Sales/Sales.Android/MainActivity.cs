using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using ImageCircle.Forms.Plugin.Droid;

namespace Sales.Droid
{
    [Activity(Label = "Sales", 
        Icon = "@drawable/ic_launcher",/*@mipmap/icon"*/
        Theme = "@style/MainTheme",
        MainLauncher = false, /*true*/
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);/*PARA LA CAMARA*/
            ImageCircleRenderer.Init();/*para imagenes en circulo*/
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        /*PARA LA CAMARA DEL TELEFONO*/
        public override void OnRequestPermissionsResult(
        int requestCode,
        string[] permissions,
        [GeneratedEnum] Permission[] grantResults)
        {
                PermissionsImplementation.Current.OnRequestPermissionsResult(
                    requestCode,
                    permissions,
                    grantResults);
        }

     



    }
}