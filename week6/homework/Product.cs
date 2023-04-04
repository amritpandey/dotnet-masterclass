
using Dapper;
using MySql.Data.MySqlClient;   

namespace HackYourFuture.Week6
{
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product>PostUser(Product productPost);
}

public class ProductRepository : IProductRepository
{
    private string connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        using var connection = new MySqlConnection(connectionString);

        var products = await connection.QueryAsync<Product>("SELECT id,name,price FROM products");
        return products;
    }

    public async Task<Product>PostProduct(Product product)
    {
        using var connection = new MySqlConnection(connectionString);

        var products = await connection.QueryAsync<Product>("INSERT INTO dapper.products (name, price) VALUES (@name, @price)", product);
        return product;
    }

        public Task<Product> PostUser(Product productPost)
        {
            throw new NotImplementedException();
        }
    }
public record Product(int Id, string Name, decimal Price);
}


