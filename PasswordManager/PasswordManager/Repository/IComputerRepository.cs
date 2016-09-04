using PasswordManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    interface IComputerRepository
    {
        Task InsertComputerAsync(Computer computer);
        Task UpdateComputerAsync(Computer computer);
        Task DeleteComputerAsync(Computer computer);
        Task<List<Computer>> SelectAllComputersAsync();
        Task<List<Computer>> SelectComputersAsync(string query);
    }
}
