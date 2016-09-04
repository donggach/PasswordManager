
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using PasswordManager.Database;
using PasswordManager.Common;
using PasswordManager.Model;
using System.Threading.Tasks;
using PasswordManager.Repository;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using PasswordManager.Droid.Adapter;
using System.IO;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using System;
using Android.Views.InputMethods;

namespace PasswordManager.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListingActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        List<ListAccountItem> lstItems;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.list);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

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

            var adapter = new ListAccountAdapter(lstItems);
            adapter.ItemClick += OnItemClick;
            recyclerView.SetAdapter(adapter);

            
        }

        void OnMenuItemClick(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        void OnMenuRightClick(object sender, EventArgs e)
        {

        }

        void OnItemClick(object sender, int position)
        {
            ListAccountItem item = lstItems[position];
            Intent intent = new Intent(this, typeof(ListingPasswordActivity));
            switch (item.Name)
            {
                case "Bank":
                    intent.PutExtra("type", "Bank");
                    StartActivity(intent);
                    break;
                case "Computer":
                    intent.PutExtra("type", "Computer");
                    StartActivity(intent);
                    break;
                case "Email":
                    intent.PutExtra("type", "Email");
                    StartActivity(intent);
                    break;
                case "Web":
                    intent.PutExtra("type", "Web");
                    StartActivity(intent);
                    break;
            }
        }

        private async Task InitializeDatabase()
        {
            string pathDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(pathDirectory, Constants.dbName);

            DbConnection oDbConnection = new DbConnection(path);

            lstItems = new List<ListAccountItem>();
            // Bank
            var bankRepository = new BankRepository(oDbConnection);
            var lstBank = await bankRepository.SelectAllBanksAsync();
            lstItems.Add(new ListAccountItem() { Name = "Bank", Count = lstBank.Count });

            // Computer
            var computerRepository = new ComputerRepository(oDbConnection);
            var lstComputer = await computerRepository.SelectAllComputersAsync();
            lstItems.Add(new ListAccountItem() { Name = "Computer", Count = lstComputer.Count });

            // Email
            var emailRepository = new EmailRepository(oDbConnection);
            var lstEmail = await emailRepository.SelectAllEmailsAsync();
            lstItems.Add(new ListAccountItem() { Name = "Email", Count = lstEmail.Count });

            // Web
            var webRepository = new WebRepository(oDbConnection);
            var lstWeb = await webRepository.SelectAllWebsAsync();
            lstItems.Add(new ListAccountItem() { Name = "Web", Count = lstWeb.Count });
        }
    }
}


