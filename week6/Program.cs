using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World csharp!");

//example to check whether the input is integer or not
app.MapPost("/product/{_id}",(string _id)=>
{
    if(int.TryParse(_id, out int id))
    {
       // 
       return Results.Ok();
    }
    else{
        return Results.BadRequest($"Id {_id} must  be a number");
    }

});

app.MapGet("/users", async (IConfiguration configuration) =>
{
    using (var connection = new MySqlConnection(configuration.GetConnectionString("Default")))
    {
        connection.Open();
        using (var command = new MySqlCommand("SELECT host,user FROM mysql.user", connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                var users = new List<User>();
                while (await reader.ReadAsync())
                {
                    var user = reader.GetString(reader.GetOrdinal("user"));
                    var host = reader.GetString(reader.GetOrdinal("host"));

                    users.Add(new User(user,host));
                }
                return users;
            }
        }
    }
});

app.MapGet("/users2", async (IConfiguration configuration) =>
{
    // starting from C# 8 you no longer need inner {}
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));

    var users = await connection.QueryAsync<User>("SELECT * FROM mysql.user");
    return Results.Ok(users);
});

app.MapGet("/products", async (IConfiguration configuration) =>
{
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var products = await connection.QueryAsync<Product>("SELECT id, name, price FROM dapper.products");
    return Results.Ok(products);
});

app.MapGet("/products/{id}", async (IConfiguration configuration, int id) =>
{
    if(Int32.IsNegative(id)){

        return Results.BadRequest("Id can't be negative");
    }
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    try
    {
        var products = await connection.QueryAsync<Product>($"SELECT id, name, price FROM dapper.products where id={id}");
    if(products.Count()==0)
    {
         return Results.BadRequest("No record found in database!!!");
    }
    return Results.Ok(products); 
    }
    catch (System.Exception)
    {
        
        return Results.NotFound();
    }
   
});

app.MapPost("/products", async (IConfiguration configuration, Product product) =>
{
    if(string.IsNullOrEmpty(product.Name))
    {
        return Results.BadRequest("empty");
    }
    if(product.Price.Equals(null))
    {
        return Results.BadRequest("Empty price");
    }
    await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var productId = await connection.ExecuteAsync("INSERT INTO dapper.products (name, price) VALUES (@name, @price)", product);
    
    // Status code 201
    return Results.Created($"/product/{productId}", product);
});

app.MapPut("/product/{id}", async (IConfiguration configuration, Product product, int id) =>
{
    await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var productId = await connection.ExecuteAsync($"UPDATE dapper.products SET name=@name, price=@price WHERE id={id}", product);
    // Status code 201
    return Results.Created($"/product/{productId}", product);
});

app.MapDelete("/product/{id}", async (IConfiguration configuration, int id) =>
{
    await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var productId = await connection.ExecuteAsync($"DELETE FROM dapper.products WHERE id=@id", new { ID = id });
    if(productId==0)
    {
       // throw new Exception("Product id not found");
       return Results.NotFound($"id no {id} not found");
    }
    return Results.Ok(productId);
});

app.MapGet("/productJoin/{id}", async (IConfiguration configuration, int id) =>
{
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    // var products = await connection.QueryAsync<Product>($"SELECT id, name, price FROM dapper.products where id={id}");
    // var sales = await connection.QueryAsync<Sale>($"SELECT id, sale_person, sale_date FROM dapper.sales where id={id}");
    var salesProduct =await connection.QueryAsync<saleProduct>($"SELECT products.id,products.name, products.price,sales.sale_person AS SalePerson,sales.sale_date as SaleDate FROM dapper.products INNER JOIN dapper.sales ON products.id=sales.product_id WHERE products.id={id}");
    return Results.Ok(salesProduct);
});

app.Run();
public record User(string user, string host)
{ public User() : this("", "") { } }

record Product(int Id, string Name, decimal Price);
record Sale(int Id, string SalePerson, DateTime SaleDate);
record saleProduct(int id, string Name, decimal Price, string SalePerson, DateTime SaleDate);


// make separate record while joing the tables
// change the array to object so that you will have a place to grow.



