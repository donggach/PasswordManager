using PasswordManager.Database;
using PasswordManager.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Repository
{
    public class EmailRepository : IEmailRepository
    {
        SQLiteAsyncConnection conn;

        public EmailRepository(IDbConnection oIDbConnection)
        {
            conn = oIDbConnection.GetAsyncConnection();
        }

        public async Task DeleteEmailAsync(Email email)
        {
            await conn.DeleteAsync(email);
        }

        public async Task InsertEmailAsync(Email email)
        {
            await conn.InsertAsync(email);
        }

        public async Task<List<Email>> SelectAllEmailsAsync()
        {
            return await conn.Table<Email>().ToListAsync();
        }

        public async Task<List<Email>> SelectEmailsAsync(string query)
        {
            return await conn.QueryAsync<Email>(query);
        }

        public async Task UpdateEmailAsync(Email email)
        {
            await conn.UpdateAsync(email);
        }
    }
}
