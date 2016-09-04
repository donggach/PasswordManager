using PasswordManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    interface IWebRepository
    {
        Task InsertWebAsync(Web web);
        Task UpdateWebAsync(Web web);
        Task DeleteWebAsync(Web web);
        Task<List<Web>> SelectAllWebsAsync();
        Task<List<Web>> SelectWebsAsync(string query);
    }
}
