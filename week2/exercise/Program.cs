

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello fuckingWorld!");
/*
1. Caclulator

Make a GET endpoint that will take parameter number1 and number2 and based on operation parameter will perform 
one of following operations: addition, substraction, multiplication. If number1 or number2 are not a number return bad request response. 
For operation valid values are add, substract, multiplay.

Example: GET /calculate?number1=5&number2=10&operation=add would return 15 as a result.
*/

app.MapGet("/calculator", (string numberOne, string numberTwo, string operation) =>
{
    var numberOneIsValid = int.TryParse(numberOne, out var parseNumberOne);
    var numberTwoIsValid = int.TryParse(numberTwo, out var parseNumberTwo);

    if (!numberOneIsValid || !numberTwoIsValid)
    {
       return Results.BadRequest("One of the numbers is not valid");
       // Console.WriteLine("not valid numbers");
    }

    int result = 0;

    switch (operation)
    {
        
        case "add":
            result = parseNumberOne + parseNumberTwo;
    break;
        case "sub":
            result = parseNumberOne - parseNumberTwo;
    break;
    case "mul":
            result = parseNumberOne * parseNumberTwo;
    break;
         default:
             return Results.BadRequest("Not valid operator!!!");
             // Console.WriteLine("not valid operators");
             
    }
    
    //Console.WriteLine(result);
    return Results.Ok(result);
});


/*
2. Method example

Make a GET endpoint that will take input parameter. If input parameter is an integer call AddNumbers method that receives input 
and returns sum of all integer values. If input is not an integer then call method CountCapitalLetters method that 
receives input and returns counter of all caputal letters.

Hint: use int.TryParse() and char.IsUpper()

Example 1: GET /?input=153 would calculate 1 + 5 + 3 and return 9. Example 2: GET /?input=The Quick Brown Fox Jumps Over the Lazy Dog will return 8.*/

// app.MapGet("/methods", (string input) => {
//     var inputIsNumber = int.TryParse(input, out var _);
//     if(inputIsNumber){
//        return Results.Ok(AddNumbers(n));
//     }else{
//        return Results.Ok(CountCapitalLetters(input));
//     }

// });



// int AddNumbers(int n)
// {
   
// var digits = n.ToString().Select(t=>int.Parse(t.ToString())).ToArray();
// Console.WriteLine(digits);
// return digits;
//    }

// }

// int CountCapitalLetters(string input)
// {
//     int count = 0;
// return null;

// }

/* 3. Distinct alphabetical list

Make a GET endpoint that takes a string as input and returns a new list containing only the unique characters, 
sorted in alphabetical order. For example, if the input string is The cool breeze 
whispered through the trees, the output should be ["b", "c", "d", "e", "h", "i", "l", "o", "p", "r", "s", "t", "u", "w", "z"].
*/
// app.MapGet("/distalphabet", (string input)=>{
//     var result = new List<char>();
//     foreach (var c in input)
//     {
//         if(char.IsLetter(c))
//         {
//             result.Add(c);
//         }
//         result.Sort();
//         return Results.Ok(result);
//     }
// });
app.Run();

