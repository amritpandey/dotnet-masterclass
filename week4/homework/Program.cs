var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello this is homework 4 World!");
/*
A POST endpoint that calls POST users/add with a record with FirstName, 
LastName and Age (this simulates creating a user)
*/
app.MapPost("/create", async (User userData) =>
{
    var httpClient = new HttpClient();
    var response= await httpClient.PostAsJsonAsync("https://dummyjson.com/users/add", userData)
        ;
        System.Console.WriteLine(response);
        var data = await response.Content.ReadFromJsonAsync<User>();
        var responseData = new ResolveData(response.StatusCode,response.ReasonPhrase,response.Data);
        // {
        //     StatusCode = response.StatusCode,
        //     ReasonPhrase = response.ReasonPhrase,
        //     data

        // };
        return (responseData);
});
/*
A POST endpoint that that calls Post products/add with a record with Title and 
Price (this simulates creating a product)
*/
app.MapPost("/createProduct", async (Product productData) =>
{
    var httpClient = new HttpClient();
    var response= await httpClient.PostAsJsonAsync("https://dummyjson.com/products/add", productData)
        ;
        System.Console.WriteLine(response);
        var data = await response.Content.ReadFromJsonAsync<Product>();

        // var responseData = new
        // {
        //     StatusCode = response.StatusCode,
        //     ReasonPhrase = response.ReasonPhrase,
        //     data
        // };
        return (responseData);
});
/*
A POST endpoint that takes a lists of ids and retrieves 
all of the users with those ids from the GET users (Id, FirstName, LastName and Age)*/
app.MapPost("/retrieveUsers", async (UserId ids) =>
{
    var httpClient = new HttpClient();
        var result = "";
        foreach (var id in ids.Id)
        
        {
            var response= await httpClient.GetAsync($"https://dummyjson.com/users/{id}");
            var data = await response.Content.ReadFromJsonAsync<User>();
             result += data.FirstName +" "+ data.LastName+" "+data.Age +" ";
            
        }
        return Results.Ok(result); 
});
/*
A POST endpoint that takes a lists of ids and retrieves all of the products 
with those ids GET products(Id, Title)
*/
app.MapPost("/retrieveProducts", async (UserId ids) =>
{
    var httpClient = new HttpClient();
        
        List<string> productList = new List<string>();
        foreach (var id in ids.Id)
        
        {
            var response= await httpClient.GetAsync($"https://dummyjson.com/products/{id}");
            var data = await response.Content.ReadFromJsonAsync<Product>();
             var result = "Title: "+ data.Title +" Price: "+ data.Price+" ";
             productList.Add(result);
        }
        return Results.Ok(productList); 
});

/*
A GET endpoint that gets a user based on an id
*/
app.MapGet("/userById", async (int id) =>
{
    var httpClient = new HttpClient();
  
            var response= await httpClient.GetAsync($"https://dummyjson.com/users/{id}");
            var data = await response.Content.ReadFromJsonAsync<User>();
            System.Console.WriteLine(data);
        return Results.Ok(data); 
});
/*
A PUT endpoint that updates a user based on an id and the body of the request
*/
app.MapPut("/updateById", async (User userBody, int id) =>
{
    var httpClient = new HttpClient();
  
            var response= await httpClient.PutAsJsonAsync($"https://dummyjson.com/users/{id}",userBody);
            var data = await response.Content.ReadFromJsonAsync<User>();
            System.Console.WriteLine(data);
        return Results.Ok(data); 
});
/*
A DELETE endpoint that deletes a user based on an id*/
app.MapDelete("/deleteById", async (int id) =>
{
    var httpClient = new HttpClient();
  
            var response= await httpClient.DeleteAsync($"https://dummyjson.com/users/{id}");
            var data = await response.Content.ReadFromJsonAsync<User>();
            System.Console.WriteLine(data);
        return Results.Ok(data); 
});

app.Run();

record User(string FirstName, string LastName, int Age);
record Product(string Title, decimal Price);
record UserId(List<int>Id );
record Product1(int Id, string Title, int Price);

public class ResolveData
{
    public System.Net.HttpStatusCode StatusCode { get; set; }
    public System.Net.Http.HttpResponseMessage ReasonPhrase { get; set; }
    public HttpResponseMessage Data { get; set; }
    
    public ResolveData(System.Net.HttpStatusCode statusCode, System.Net.Http.HttpResponseMessage reasonPhrase, HttpResponseMessage data)
    {
       StatusCode= statusCode;
       ReasonPhrase=reasonPhrase;
       Data=data;

    } 
}