using SQLite;
using System.Threading.Tasks;

namespace PasswordManager.Database
{
    public interface IDbConnection
    {
        Task InitializeDatabase();
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
