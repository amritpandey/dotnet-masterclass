var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Worllladold!");

// Complete the solution so that it reverses the string passed into it.

app.MapGet("/q1", (string input) => 
{

char[] charArray = input.ToCharArray();
Array.Reverse( charArray );
string reversed = new string( charArray );
Console.WriteLine($"Reversed input value: {reversed}");
}
);

app.MapGet("/add", (int num1, int num2)=>num1+num2);


app.Run();
