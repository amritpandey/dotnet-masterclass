using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week7;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllReviews();
    Task<IEnumerable<Review>> ReviewMealWithId(int id);
    Task<Review> AddReview(Review review);

}

public class ReviewRepository : IReviewRepository
{
    private string connectionString;

    public ReviewRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<Review>> GetAllReviews()
    {
        using var connection = new MySqlConnection(connectionString);
        var reviews = await connection.QueryAsync<Review>("SELECT * FROM Review");
        return reviews;
    }

    public async Task<IEnumerable<Review>> ReviewMealWithId(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        var reviews = await connection.QueryAsync<Review>($"SELECT * FROM review WHERE meal_id = @id", new { id });
        return reviews;
    }

    public async Task<Review> AddReview(Review review)
    {
        using var connection = new MySqlConnection(connectionString);
        var reviews = await connection.ExecuteAsync("INSERT INTO review (title,description,stars,created_date,meal_id) VALUES (@title,@description,@stars,@createdDate,@mealId)", review);
        return review;
    }

}

public class Review
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Stars { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("meal_id")]
    public int MealId { get; set; }

}


