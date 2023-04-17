using HackYourFuture.Week7;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

var app = builder.Build();
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

//meal
app.MapGet("/api/meals", async (IMealRepository mealRepository) =>
{
    return await mealRepository.GetAllMeals();
});

app.MapGet("/api/meals/{text}", async (IMealRepository mealRepository, string text) =>
{
    return await mealRepository.SearchMeal(text);
});

app.MapDelete("/api/meals/{id}", async (IMealRepository mealRepository, int id) =>
 {
     return await mealRepository.DeleteMeal(id);
 });

 app.MapPost("/api/meals", async (IMealRepository mealRepository, Meal meal) =>
{
    return await mealRepository.AddMeal(meal);
});

 app.MapGet("/api/meals/top-meal", async (IMealRepository mealRepository) =>
{
    return await mealRepository.TopMeal();
});

//reservation
app.MapGet("/api/reservations", async (IReservationRepository reservationRepository) =>
{
    return await reservationRepository.GetAllReservations();
});

//review
app.MapGet("/api/reviews", async(IReviewRepository reviewRepository)=>
{
    return await reviewRepository.GetAllReviews();
});


app.MapGet("/api/reviews/{id}", async (IReviewRepository reviewRepository, int id) =>
 {
     return await reviewRepository.ReviewMealWithId(id);
 });

  app.MapPost("/api/reviews", async (IReviewRepository reviewRepository, Review review) =>
{
    return await reviewRepository.AddReview(review);
});



app.Run();

