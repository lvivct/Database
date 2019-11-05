using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Login.Models;
using System.Security.Cryptography;

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
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLite.SQLiteConnection(App.FilePath))
                Logins.ItemsSource = conn.Table<UserData>().ToList();

        }

        public void Saved(object sender, EventArgs e)
        {
            UserData newUser = new UserData() { Login = EntryLogin.Text, Hash = HashPassword(EntryPassword.Text) };
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
                if (temp[i].Login == EntryLogin.Text && VerifyHashedPassword(temp[i].Hash, EntryPassword.Text))
                {
                    constant.Hash = temp[i].Hash;
                    constant.Login = temp[i].Login;
                    await Navigation.PushAsync(new Page1());
                    break;
                }
            EntryLogin.Text = "";
            EntryPassword.Text = "";
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return true;
        }
    }
}
