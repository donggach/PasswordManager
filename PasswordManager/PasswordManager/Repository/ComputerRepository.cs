using PasswordManager.Database;
using PasswordManager.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    public class ComputerRepository : IComputerRepository
    {
        SQLiteAsyncConnection conn;

        public ComputerRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task DeleteComputerAsync(Computer bomputer)
        {
            await conn.DeleteAsync(bomputer);
        }

        public async Task InsertComputerAsync(Computer bomputer)
        {
            await conn.InsertAsync(bomputer);
        }

        public async Task<List<Computer>> SelectAllComputersAsync()
        {
            return await conn.Table<Computer>().ToListAsync();
        }

        public async Task<List<Computer>> SelectComputersAsync(string query)
        {
            return await conn.QueryAsync<Computer>(query);
        }

        public async Task UpdateComputerAsync(Computer bomputer)
        {
            await conn.UpdateAsync(bomputer);
        }
    }
}
