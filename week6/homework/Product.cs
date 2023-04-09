using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> PostProduct(Product productPost);
        Task<Product> PutProduct(Product product);
        Task<int> DeleteProduct(int id);
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

        public async Task<Product> PostProduct(Product product)
        {
            using var connection = new MySqlConnection(connectionString);
            var products = await connection.ExecuteAsync("INSERT INTO products (name, price) VALUES (@name, @price)", product);
            return product;
        }

        public async Task<Product> PutProduct(Product product)
        {
            using var connection = new MySqlConnection(connectionString);
            var products = await connection.ExecuteAsync($"UPDATE products set name=@name, price=@price WHERE id=@id", product);
            return product;
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            var products = await connection.ExecuteAsync($"DELETE FROM products WHERE id=@id", new { id });
            return products;
        }

    }
    public record Product(int Id, string Name, decimal Price);
}


