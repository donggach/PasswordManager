using PasswordManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    interface IBankRepository
    {
        Task InsertBankAsync(Bank bank);
        Task UpdateBankAsync(Bank bank);
        Task DeleteBankAsync(Bank bank);
        Task<List<Bank>> SelectAllBanksAsync();
        Task<List<Bank>> SelectBanksAsync(string query);
    }
}
