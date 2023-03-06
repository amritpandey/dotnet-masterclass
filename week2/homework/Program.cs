var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

/*
Write a GET endpoint that takes a string as input and counts the frequency of each word 
in the string. Your program should output a list of objects where each object contains 
a word and its frequency. For example, if the input string is "the quick brown fox jumps 
over the lazy dog", your program should output the following list: [("the", 2), ("quick", 1), 
("brown", 1), ("fox", 1), ("jumps", 1), ("over", 1), ("lazy", 1), ("dog", 1)]
*/

app.MapGet("/wfc", () =>
{


    string Word = "the quick brown fox jumps over the lazy dog";
    var Value = Word.Split(' ');  // Split the string using 'Space' and stored it an var variable  
    Dictionary<string, int> RepeatedWordCount = new Dictionary<string, int>();
    for (int i = 0; i < Value.Length; i++) //loop the splited string  
    {
        if (RepeatedWordCount.ContainsKey(Value[i])) // Check if word already exist in dictionary update the count  
        {
            RepeatedWordCount[Value[i]]++;
        }
        else
        {
            RepeatedWordCount[Value[i]] = 1;
        }
    }

    return Results.Ok(RepeatedWordCount);


});
app.Run();


