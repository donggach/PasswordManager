using PasswordManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    interface IEmailRepository
    {
        Task InsertEmailAsync(Email email);
        Task UpdateEmailAsync(Email email);
        Task DeleteEmailAsync(Email email);
        Task<List<Email>> SelectAllEmailsAsync();
        Task<List<Email>> SelectEmailsAsync(string query);
    }
}
