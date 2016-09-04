using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.Model;
using PasswordManager.Database;
using SQLite;

namespace PasswordManager.Repository
{
    public class UserRepository : IUserRepository
    {
        SQLiteAsyncConnection conn;

        public UserRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            return await conn.DeleteAsync(user);
        }

        public async Task<int> InsertUserAsync(User user)
        {
           return await conn.InsertAsync(user);
        }

        public async Task<List<User>> SelectAllUsersAsync()
        {
            return await conn.Table<User>().ToListAsync();
        }

        public async Task<List<User>> SelectUsersAsync(string query)
        {
            return await conn.QueryAsync<User>(query);
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            return await conn.UpdateAsync(user);
        }
    }
}
