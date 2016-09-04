
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using PasswordManager.Database;
using PasswordManager.Common;
using PasswordManager.Model;
using System.Threading.Tasks;
using PasswordManager.Repository;
using System.Collections.Generic;
using Android.Widget;
using System;
using System.IO;
using System.Linq;
using Android.Views;
using Android.Content.PM;

namespace PasswordManager.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "Password Manager", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        UserRepository userRepository;
        Button btnRegister;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            //FindViewById<ImageView>(Resource.Id.btnMenu);
            //FindViewById<ImageView>(Resource.Id.btnMenuRight);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnLogin.Click += OnLoginItemClick;
            btnRegister.Click += OnRegisterClick;

            await InitializeDatabase();
            //await InsertBanks();
        }

        void OnLoginItemClick(object sender, EventArgs e)
        {
            StartActivity(typeof(ListingActivity));
        }

        void OnRegisterClick(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private async Task InitializeDatabase()
        {
            string pathDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(pathDirectory, Constants.dbName);

            DbConnection oDbConnection = new DbConnection(path);
            if (!File.Exists(path))
                await oDbConnection.InitializeDatabase();

            userRepository = new UserRepository(oDbConnection);
            List<User> lstUser = await userRepository.SelectAllUsersAsync();
            if (lstUser.Any())
            {
                btnRegister.Visibility = ViewStates.Invisible;
            }

            //await InsertBanks(oDbConnection);
            //await InserEmail(oDbConnection);
            //await InserComputer(oDbConnection);
            //await InserWeb(oDbConnection);

        }

        //private async Task InsertBanks(DbConnection oDbConnection)
        //{
        //    List<Bank> banktList = new List<Bank>();
        //    banktList.Add(new Bank {Id = 1, Name = "Bank1" ,Account = "Ducdv 1", ATMPassword = "123", InternetUser = "M01", InternetPassword = "1234" });
        //    banktList.Add(new Bank { Id = 2, Name = "Bank2", Account = "Ducdv 2", ATMPassword = "123", InternetUser = "M01", InternetPassword = "1234" });
        //    banktList.Add(new Bank { Id = 3, Name = "Bank3", Account = "Ducdv 3", ATMPassword = "123", InternetUser = "M01", InternetPassword = "1234" });
        //    banktList.Add(new Bank { Id = 4, Name = "Bank4", Account = "Ducdv 4", ATMPassword = "123", InternetUser = "M01", InternetPassword = "1234" });

        //    var bankRepository = new BankRepository(oDbConnection);

        //    foreach (Bank item in banktList)
        //    {
        //        await bankRepository.InsertBankAsync(item);
        //    }
        //}

        //private async Task InserEmail(DbConnection oDbConnection)
        //{
        //    List<Email> emailtList = new List<Email>();
        //    emailtList.Add(new Email { Id = 1, Name = "Ducdv1",Password = "123", UserName = "User1" });
        //    emailtList.Add(new Email { Id = 2, Name = "Ducdv1", Password = "123", UserName = "User1" });

        //    var emailRepository = new EmailRepository(oDbConnection);

        //    foreach (Email item in emailtList)
        //    {
        //        await emailRepository.InsertEmailAsync(item);
        //    }
        //}

        //private async Task InserComputer(DbConnection oDbConnection)
        //{
        //    List<Computer> computerList = new List<Computer>();
        //    computerList.Add(new Computer { Id = 1, Name = "Ducdv1", Password = "123", UserName = "User1" });
        //    computerList.Add(new Computer { Id = 2, Name = "Ducdv1", Password = "123", UserName = "User1" });

        //    var computerRepository = new ComputerRepository(oDbConnection);

        //    foreach (Computer item in computerList)
        //    {
        //        await computerRepository.InsertComputerAsync(item);
        //    }
        //}

        //private async Task InserWeb(DbConnection oDbConnection)
        //{
        //    List<Web> webList = new List<Web>();
        //    webList.Add(new Web { Id = 1, Name = "Ducdv1", Password = "123", UserName = "User1" });

        //    var webRepository = new WebRepository(oDbConnection);

        //    foreach (Web item in webList)
        //    {
        //        await webRepository.InsertWebAsync(item);
        //    }
        //}
    }
}


