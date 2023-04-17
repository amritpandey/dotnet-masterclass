
using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week7;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllReservations();
}

public class ReservationRepository : IReservationRepository
{
    private string connectionString;

    public ReservationRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.QueryAsync<Reservation>("SELECT * FROM reservation");
        return meals;
    }
}



public class Reservation
{

    public int ID { get; set; }
    [JsonPropertyName("number_of_guests")]
    public int NumberOfGuests { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("contact_phonenumber")]
    public int? ContactPhonenumber { get; set; }
    [JsonPropertyName("contact_name")]
    public string? ContactName { get; set; }
    [JsonPropertyName("contact_email")]
    public string? ContactEmail { get; set; }


    public Reservation() { }
    public Reservation(int id, int numberOfGuests, DateTime createdDate, int contactPhonenumber, string contactName, string contactEmail)
    {
        ID = id;
        NumberOfGuests = numberOfGuests;
        CreatedDate = createdDate;
        ContactPhonenumber = contactPhonenumber;
        ContactName = contactName;
        ContactEmail = contactEmail;
    }
}

