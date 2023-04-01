namespace HackYourFuture.Week6;
using Dapper;
using MySql.Data.MySqlClient;   

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
}

public class UserRepository : IUserRepository
{
    private string connectionString;

    public UserRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        using var connection = new MySqlConnection(connectionString);

        var users = await connection.QueryAsync<User>("SELECT user FROM mysql.user");
        return users;
    }
}

record User(string FirstName, string LastName, int Age);