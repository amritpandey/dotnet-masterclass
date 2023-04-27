
using HackYourFuture.Week6;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World this is week6 v2!");

//user table
app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.GetUsers();
});

app.MapPost("/users", async (IUserRepository userRepository, User user) =>
{
    return await userRepository.CreateUser(user);
});

app.MapPut("/users", async (IUserRepository userRepository, User user) =>
{
    return await userRepository.UpdateUser(user);
});

app.MapDelete("/users/{id}", async (IUserRepository userRepository, int id) =>
 {
     return await userRepository.DeleteUser(id);
 });

// product table
app.MapGet("/products", async (IProductRepository productRepository) =>
{
    return await productRepository.GetProducts();
});

app.MapPost("/products", async (IProductRepository productRepository, Product product) =>
{
    return await productRepository.CreateProduct(product);
});

app.MapPut("/products", async (IProductRepository productRepository, Product product) =>
{
    return await productRepository.UpdateProduct(product);
});

app.MapDelete("/products/{id}", async (IProductRepository productRepository, int id) =>
 {
     return await productRepository.DeleteProduct(id);
 });
app.Run();

