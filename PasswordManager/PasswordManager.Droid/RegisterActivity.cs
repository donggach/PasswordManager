
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using PasswordManager.Database;
using PasswordManager.Common;
using PasswordManager.Model;
using System.Threading.Tasks;
using PasswordManager.Repository;
using System.Collections.Generic;
using People.Droid;
using Android.Widget;
using System;
using Android.Content.PM;
using Android.Content;

namespace PasswordManager.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class RegisterActivity : AppCompatActivity
    {
        UserRepository userRepository;
        EditText txtUsername;
        EditText txtPassword;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.account);

            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);

            ImageButton btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            ImageButton btnCancel = FindViewById<ImageButton>(Resource.Id.btnCancel);
            btnAdd.Click += OnAddClick;
            btnCancel.Click += OnCancelClick;

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

            string status = Intent.GetStringExtra("status");
            if(string.IsNullOrEmpty(status))
            {
                btnRightMenu.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        void OnMenuItemClick(object sender, EventArgs e)
        {
           string status = Intent.GetStringExtra("status");
           if(!string.IsNullOrEmpty(status))
            {
                OnBackPressed();
            }else
            {
                var intentComputer = new Intent(this, typeof(MainActivity));
                StartActivity(intentComputer);
            }
        }

        void OnMenuRightClick(object sender, EventArgs e)
        {

        }

        void OnCancelClick(object sender, EventArgs e)
        {

        }

        async void OnAddClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtUsername.Text))
            {

                var path = FileAccessHelper.GetLocalFilePath(Common.Constants.dbName);
                DbConnection oDbConnection = new DbConnection(path);

                User user = new User { UserName = txtUsername.Text.Trim(), Password = txtPassword.Text.Trim() };
                userRepository = new UserRepository(oDbConnection);
                int count = await userRepository.InsertUserAsync(user);
                if (count > 0)
                {
                    // redirect to login
                    var intentComputer = new Intent(this, typeof(MainActivity));
                    StartActivity(intentComputer);
                }
                else
                {
                    Toast.MakeText(this, "Cannot insert!", ToastLength.Long).Show();
                }

            }
        }
    }
}


