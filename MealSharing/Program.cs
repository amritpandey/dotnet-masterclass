using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// var mealTest =  app.Services.GetService<IMealService>();
// mealTest.AddMeal(new Meal(){Headline="test",ImageUrl="image",BodyText="body",Location="cph",Price=22});
//app.MapPost("/meals", (Meal mealTest,[FromServices] IMealService mealSharingService) =>  mealSharingService.AddMeal(mealTest) );

//Meal meal =  new Meal(){Headline="test",ImageUrl="test image",BodyText="test body",Location="scph",Price=123};
app.MapGet("/meals", ([FromServices] IMealService mealSharingService) => { return mealSharingService.ListMeals(); });
app.MapPost("/meals", ([FromServices] IMealService mealSharingService, Meal meal) => mealSharingService.AddMeal(meal));

app.Run();

public interface IMealService
{
    List<Meal> ListMeals();
    void AddMeal(Meal meal);
}

public class Meal
{
    public string Headline { get; set; }
    public string ImageUrl { get; set; }
    public string BodyText { get; set; }
    public string Location { get; set; }
    public int Price { get; set; }
}


public class FileMealService : IMealService
{
    //public List<Meal> meals = new List<Meal>();

    public List<Meal> ListMeals()
    {
        var readMeals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText("meals.json"));

        return readMeals;
    }

    public void AddMeal(Meal meal)
    {
        if (!File.Exists("meals.json"))
        {
            File.WriteAllText("meals.json", "[]");
        }

       var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText("meals.json"));

        meals.Add(meal);


        var mealPostJson = System.Text.Json.JsonSerializer.Serialize(meals);

        File.WriteAllText("meals.json", mealPostJson);
       // meals.Add(meal);
    }
}





