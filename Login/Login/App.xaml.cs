using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Views;
using Login.Models;

namespace Login
{
    public partial class App : Application
    {
        public static string FilePath;
        public App()
        {
          
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
        }
        public App(string filePath)
        {
          
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());

            FilePath = filePath;
        }

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
    }
}
