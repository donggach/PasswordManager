using PasswordManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    interface IUserRepository
    {
        Task<int> InsertUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(User user);
        Task<List<User>> SelectAllUsersAsync();
        Task<List<User>> SelectUsersAsync(string query);
    }
}
