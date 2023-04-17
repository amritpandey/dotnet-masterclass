using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week7;

public interface IMealRepository
{
    Task<IEnumerable<Meal>> GetAllMeals();
    Task<IEnumerable<Meal>> SearchMeal(string text);
    Task<int> DeleteMeal(int id);
    Task<Meal> AddMeal(Meal mealPost);
    Task<IEnumerable<Meal>> TopMeal();
}

public class MealRepository : IMealRepository
{
    private string connectionString;

    public MealRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<Meal>> GetAllMeals()
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.QueryAsync<Meal>("SELECT * FROM meal");
        return meals;
    }

    public async Task<IEnumerable<Meal>> SearchMeal(string text)
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.QueryAsync<Meal>($"SELECT * FROM meal WHERE title LIKE '%{text}%'");
        return meals;
    }

    public async Task<int> DeleteMeal(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.ExecuteAsync($"DELETE FROM meal WHERE id=@id", new { id });
        return meals;
    }

    public async Task<Meal> AddMeal(Meal meal)
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.ExecuteAsync("INSERT INTO meal (title,description,location,`when`,max_reservations,price,created_date) VALUES (@title,@description,@location,@when,@maxReservations,@price,@createdDate)", meal);
        return meal;
    }

    public async Task<IEnumerable<Meal>> TopMeal()
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.QueryAsync<Meal>("SELECT meal.id,meal.description,meal.title,meal.price,review.stars FROM meal INNER JOIN review ON meal.id=review.meal_id WHERE review.stars>4");
        return meals;
    }
}


public class Meal
{
    public int ID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime When { get; set; }
    [JsonPropertyName("max_reservations")]
    public int MaxReservations { get; set; }
    public decimal? Price { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    public Meal() { }
    public Meal(int id, string title, string description, string location, DateTime when, int maxReservations, decimal price, DateTime createdDate)
    {
        ID = id;
        Title = title;
        Description = description;
        Location = location;
        When = when;
        MaxReservations = maxReservations;
        Price = price;
        CreatedDate = createdDate;
    }
}

