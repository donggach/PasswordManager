
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

namespace PasswordManager.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListingPasswordActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        List<ListAccountItem> lstItems;
        string type;
        TextView textTitle;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.passwordlist);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            ImageView btnMenu = FindViewById<ImageView>(Resource.Id.btnMenu);
            ImageView btnRightMenu = FindViewById<ImageView>(Resource.Id.btnMenuRight);
            ImageView btnAdd = FindViewById<ImageView>(Resource.Id.btnAddList);
             textTitle = FindViewById<TextView>(Resource.Id.textView11);

            btnMenu.Click += OnMenuItemClick;
            btnAdd.Click += OnAddClick;
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

            var adapter = new ListPasswordAdapter(lstItems);
            adapter.ItemClick += OnItemClick;
            recyclerView.SetAdapter(adapter);

            
        }

        void OnAddClick(object sender, EventArgs e)
        {
            switch (type)
            {
                case "Bank":
                    var intent = new Intent(this, typeof(BankActivity));
                    StartActivity(intent);
                    break;
                case "Computer":
                    var intentComputer = new Intent(this, typeof(ComputerActivity));
                    StartActivity(intentComputer);
                    break;
                case "Email":
                    var intentEmail = new Intent(this, typeof(EmailActivity));
                    StartActivity(intentEmail);
                    break;
                case "Web":
                    var intentWeb = new Intent(this, typeof(WebActivity));
                    StartActivity(intentWeb);
                    break;
            }
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
            switch (type)
            {
                case "Bank":
                    var intent = new Intent(this, typeof(BankActivity));
                    intent.PutExtra("id", item.Id);
                    StartActivity(intent);
                    break;
                case "Computer":
                    var intentComputer = new Intent(this, typeof(ComputerActivity));
                    intentComputer.PutExtra("id", item.Id);
                    StartActivity(intentComputer);
                    break;
                case "Email":
                    var intentEmail = new Intent(this, typeof(EmailActivity));
                    intentEmail.PutExtra("id", item.Id);
                    StartActivity(intentEmail);
                    break;
                case "Web":
                    var intentWeb = new Intent(this, typeof(WebActivity));
                    intentWeb.PutExtra("id", item.Id);
                    StartActivity(intentWeb);
                    break;
            }
        }

        private async Task InitializeDatabase()
        {
            string pathDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(pathDirectory, Constants.dbName);

            DbConnection oDbConnection = new DbConnection(path);

            lstItems = new List<ListAccountItem>();
            type = Intent.GetStringExtra("type");
            switch (type)
            {
                case "Bank":

                    textTitle.Text = "Bank Password List";

                    // Bank
                    var bankRepository = new BankRepository(oDbConnection);
                    var lstBank = await bankRepository.SelectAllBanksAsync();
                    foreach(Bank bank in lstBank)
                    {
                        lstItems.Add(new ListAccountItem() { Id = bank.Id, Name = bank.Name, Account = bank.Account});
                    }
                    
                    break;
                case "Computer":

                    textTitle.Text = "Computer Password List";

                    // Computer
                    var computerRepository = new ComputerRepository(oDbConnection);
                    var lstComputer = await computerRepository.SelectAllComputersAsync();
                    foreach (Computer computer in lstComputer)
                    {
                        lstItems.Add(new ListAccountItem() { Id = computer.Id, Name = computer.Name, Account = computer.UserName });
                    }

                    break;
                case "Email":
                    textTitle.Text = "Email Password List";

                    // Email
                    var emailRepository = new EmailRepository(oDbConnection);
                    var lstEmail = await emailRepository.SelectAllEmailsAsync();
                    foreach (Email email in lstEmail)
                    {
                        lstItems.Add(new ListAccountItem() { Id = email.Id, Name = email.Name, Account = email.UserName });
                    }

                    break;
                case "Web":

                    textTitle.Text = "Web Password List";
                    // Web
                    var webRepository = new WebRepository(oDbConnection);
                    var lstWeb = await webRepository.SelectAllWebsAsync();
                    foreach (Web web in lstWeb)
                    {
                        lstItems.Add(new ListAccountItem() { Id = web.Id, Name = web.Name, Account = web.UserName });
                    }

                    break;
            }
        }
    }
}


