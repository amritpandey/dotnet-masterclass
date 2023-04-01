using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World this is week6 v2!");

app.MapGet("/users2", async (IConfiguration configuration) =>
{
    // starting from C# 8 you no longer need inner {}
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));

    var users = await connection.QueryAsync<User>("SELECT user FROM mysql.user");
    return users;
});

app.Run();

