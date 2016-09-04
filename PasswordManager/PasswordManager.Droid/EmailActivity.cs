
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using PasswordManager.Database;
using PasswordManager.Common;
using PasswordManager.Model;
using System.Threading.Tasks;
using PasswordManager.Repository;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Widget;
using System;
using Android.Views.InputMethods;
using Android.Content;

namespace PasswordManager.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class EmailActivity : AppCompatActivity
    {
        BankRepository bankRepository;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.email);

            ImageView btnMenu = FindViewById<ImageView>(Resource.Id.btnMenu);
            ImageView btnRightMenu = FindViewById<ImageView>(Resource.Id.btnMenuRight);
            btnMenu.Click += OnMenuItemClick;
            btnRightMenu.Click += (s, arg) => {

                Android.Support.V7.Widget.PopupMenu menu = new Android.Support.V7.Widget.PopupMenu(this, btnRightMenu);
                menu.Inflate(Resource.Menu.popup_menu);
                menu.MenuItemClick += (s1, arg1) => {
                    if (arg1.Item.TitleFormatted.ToString().Equals("Edit user"))
                    {
                        var intentComputer = new Intent(this, typeof(RegisterActivity));
                        intentComputer.PutExtra("status", "edit");
                        StartActivity(intentComputer);
                    }
                    else if (arg1.Item.TitleFormatted.ToString().Equals("Sign out"))
                    {
                        var intentComputer = new Intent(this, typeof(MainActivity));
                        StartActivity(intentComputer);
                    }
                };

                menu.DismissEvent += (s2, arg2) => {

                };

                menu.Show();
            };

            await InitializeDatabase();

            HideKeyboard();
        }

        void OnMenuItemClick(object sender, EventArgs e)
        {
            HideKeyboard();
            OnBackPressed();
        }

        void OnMenuRightClick(object sender, EventArgs e)
        {

        }

        private async Task InitializeDatabase()
        {
            string pathDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(pathDirectory, Constants.dbName);

            DbConnection oDbConnection = new DbConnection(path);
            await oDbConnection.InitializeDatabase();
            bankRepository = new BankRepository(oDbConnection);
        }

        private void HideKeyboard()
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}


