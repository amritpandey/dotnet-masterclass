
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> CreateUser(User userPost);
        Task<User> UpdateUser(User user);
        Task<int> DeleteUser(int id);
    }

    public class UserRepository : IUserRepository
    {
        private string connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using var connection = new MySqlConnection(connectionString);
            var users = await connection.QueryAsync<User>("SELECT id,name,phone FROM users1");
            return users;
        }

        public async Task<User> PostUser(User user)
        {
            using var connection = new MySqlConnection(connectionString);
            var users = await connection.ExecuteAsync("INSERT INTO dapper.users1 (name, phone) VALUES (@name, @phone)", user);
            return user;
        }

        public async Task<User> PutUser(User user)
        {
            using var connection = new MySqlConnection(connectionString);
            var users = await connection.ExecuteAsync($"UPDATE users1 set name=@name, phone=@phone WHERE id=@id", user);
            return user;
        }

        public async Task<int> DeleteUser(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            var users = await connection.ExecuteAsync($"DELETE FROM users1 WHERE id=@id", new { id });
            return users;
        }
    }
    public record User(int id, string name, int phone);
}


