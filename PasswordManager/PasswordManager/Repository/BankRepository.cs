using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Model;
using PasswordManager.Database;
using SQLite;

namespace PasswordManager.Repository
{
    public class BankRepository : IBankRepository
    {
        SQLiteAsyncConnection conn;

        public BankRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task DeleteBankAsync(Bank bank)
        {
            await conn.DeleteAsync(bank);
        }

        public async Task InsertBankAsync(Bank bank)
        {
            await conn.InsertAsync(bank);
        }

        public async Task<List<Bank>> SelectAllBanksAsync()
        {
            return await conn.Table<Bank>().ToListAsync();
        }

        public async Task<List<Bank>> SelectBanksAsync(string query)
        {
            return await conn.QueryAsync<Bank>(query);
        }

        public async Task UpdateBankAsync(Bank bank)
        {
            await conn.UpdateAsync(bank);
        }
    }
}
