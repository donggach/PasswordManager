using PasswordManager.Database;
using PasswordManager.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    public class WebRepository : IWebRepository
    {
        SQLiteAsyncConnection conn;

        public WebRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task DeleteWebAsync(Web web)
        {
            await conn.DeleteAsync(web);
        }

        public async Task InsertWebAsync(Web web)
        {
            await conn.InsertAsync(web);
        }

        public async Task<List<Web>> SelectAllWebsAsync()
        {
            return await conn.Table<Web>().ToListAsync();
        }

        public async Task<List<Web>> SelectWebsAsync(string query)
        {
            return await conn.QueryAsync<Web>(query);
        }

        public async Task UpdateWebAsync(Web web)
        {
            await conn.UpdateAsync(web);
        }
    }
}
