
using Dapper;
using MySql.Data.MySqlClient;   

namespace HackYourFuture.Week6
{
public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User>PostUser(User userPost);
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

        var users = await connection.QueryAsync<User>("SELECT first_name as FirstName,last_name as LastName,phone as Phone FROM dapper.users");
        return users;
    }

    public async Task<User>PostUser(User user)
    {
        using var connection = new MySqlConnection(connectionString);

        var users = await connection.QueryAsync<User>("INSERT INTO dapper.users (name, price) VALUES (@name, @price)", user);
        return user;
    }
    
}
public record User(string FirstName, string LastName,int Phone);
}


