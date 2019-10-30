using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Login.Models;

namespace Login.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {            

            InitializeComponent();

//this.fun();
        }
        
        public void fun()
        {
            var conn = new SQLite.SQLiteConnection(App.FilePath);
            Logins.ItemsSource = conn.Table<UserData>().ToList();
        }
        public void Saved(object sender, EventArgs e)
        {
            UserData newUser = new UserData() { Login = EntryLogin.Text, Password = EntryPassword.Text };
            if (DatabaseHelp.Insert(ref newUser, App.FilePath) && newUser.Corect())
            {
                DisplayAlert("OK", "OK", "OK");
                using (var conn = new SQLite.SQLiteConnection(App.FilePath))
                    Logins.ItemsSource = conn.Table<UserData>().ToList();
            }
            else
            {
                EntryLogin.Text = "";
                EntryPassword.Text = "";
                DisplayAlert("ERORR", "ERORR", "ERORR");
            }

        }

        async void logined(object sender, EventArgs e)
        {
            List<UserData> temp;
            using (var conn = new SQLite.SQLiteConnection(App.FilePath))
                temp = conn.Table<UserData>().ToList();

            for (int i = 0; i < temp.Count(); i++)
                if (temp[i].Login == EntryLogin.Text && temp[i].Password == EntryPassword.Text)
                {
                    await Navigation.PushAsync(new Page1());
                    break;
                }
            EntryLogin.Text = "";
            EntryPassword.Text = "";
        }
    }
}
