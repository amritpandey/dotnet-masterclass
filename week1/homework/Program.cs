var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Worllladold!");

// Complete the solution so that it reverses the string passed into it.

app.MapGet("/q1", (string input) =>
{

    char[] charArray = input.ToCharArray();
    Array.Reverse(charArray);
    string reversed = new string(charArray);
    Console.WriteLine($"Reversed input value: {reversed}");
}
);


// Return the number (count) of vowels in the given string. Consider a, e, i, o, u as vowels.

app.MapGet("/q2", () =>
{
    string input = "amrit";
    char[] charArray = input.ToCharArray();
    int vowelCount = 0;
    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] == 'a' || input[i] == 'e' || input[i] == 'i' || input[i] == 'o' || input[i] == 'u')
        {
            vowelCount++;
        }
    }

    Console.WriteLine($"Number of vowels: {vowelCount}");
});

// Given an array of the numbers return an array with two elements where first element represents sum of all negative numbers 
// and second element represents multiplication of all positive numbers;

app.MapGet("/q3", () =>
{
    int[] arr = new[] { 271, -3, 1, 14, -100, 13, 2, 1, -8, -59, -1852, 41, 5 };
    int negativeNumbers = 0;
    int positiveNumbers = 1;

    int[] result = new int[2];
    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] > 0)
        {
            positiveNumbers *= arr[i];
        }
        else
        {
            negativeNumbers += arr[i];
        }
        result[0] = negativeNumbers;
        result[1] = positiveNumbers;

    }

    Console.WriteLine($"Sum of negative numbers: {result[0]}. Multiplication of positive numbers: {result[1]}");
});
/*
4. Classical task

Create function Fibonacci that returns N'th element of Fibonacci sequence (classic programming task).
*/

app.MapGet("/q4", (int n) =>
{

    int nthNumber = Fibonacci(n);
    Console.WriteLine($"Nth fibonacci number is {nthNumber}");
    return $"Nth fibonacci number is {nthNumber}";

    int Fibonacci(int num)
    {
        int a = 0, b = 1, c = 0;
        for (int i = 2; i < n; i++)
        {
            c = a + b;
            a = b;
            b = c;

        }
        return c;
    }
});


app.Run();
