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


// Return the number (count) of vowels in the given string. Consider a, e, i, o, u as vowels.

app.MapGet("/q2", ()=>{
    string input = "amrit";
    char[] charArray = input.ToCharArray();
            int vowelCount = 0;
            for (int i = 0; i < input.Length; i++)
    {
        if (input[i]  == 'a' || input[i] == 'e' || input[i] == 'i' || input[i] == 'o' || input[i] == 'u')
        {
            vowelCount++;
        }
    }
           
    Console.WriteLine($"Number of vowels: {vowelCount}");
});


    
app.Run();
