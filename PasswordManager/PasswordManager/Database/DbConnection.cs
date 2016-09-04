using PasswordManager.Model;
using SQLite;
using System;
using System.Threading.Tasks;

namespace PasswordManager.Database
{
    public class DbConnection : IDbConnection
    {
        SQLiteAsyncConnection conn;

        public DbConnection(string path)
        {
            conn =  new SQLiteAsyncConnection(path);
        }

        public async Task InitializeDatabase()
        {
            await conn.CreateTableAsync<Bank>();
            await conn.CreateTableAsync<Computer>();
            await conn.CreateTableAsync<Email>();
            await conn.CreateTableAsync<Web>();
            await conn.CreateTableAsync<User>();
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return conn;
        }
    }
}
