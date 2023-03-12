var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World, this is 3rd exercise!");

app.MapGet("/ctf", () =>
{
    var temp = new Temperature(75);
    var result = temp.CelsiusToFarenheit();
    System.Console.WriteLine($"{temp.Degrees} celsius is equal {result} Farenheit");
    var result1 = temp.FarenheitToCelsius();
    System.Console.WriteLine($"{temp.Degrees} Farenheit is equal {result1} celsius");
});

app.MapGet("/cc", () =>
{
    var amount = -100;
    var exchangeRate = new ExchangeRate("EUR", "DKK");
    exchangeRate.Rate = 7.5m;
    Console.WriteLine($"{amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}");
});

app.MapGet("/interface", () =>
{
    var cow = new Cow();
    cow.MakeSound();
    var dog = new Dog();
    dog.MakeSound();
    var cat = new Cat();
    cat.MakeSound();
});

app.MapGet("/acc", () =>
{
    var account = new Account(100);
    account.Deposit(200);
    Console.WriteLine($"Account balance is {account.Balance}");
    account.Withdraw(20);
    Console.WriteLine($"Account balance is {account.Balance}");
    account.Withdraw(100);
    Console.WriteLine($"Account balance is {account.Balance}");

});
app.Run();

/*
Create a class named Temperature and make a constructor with one decimal parameter - degrees (Celsius) 
and assign its value to Property. The property has a public getter and private setter. 
The property setter throws an exception if temperature is less than 273.15 Celsius. 
Then, create a property Fahrenheit that will convert Celsius to Fahrenheit (it has no setter similar to NicePrintout)
Bonus: create Kelvin property */

public class Temperature
{
    private decimal _degrees;//backing field
    
    public Temperature(decimal degrees)
    {
        Degrees = degrees;
    }

    public decimal Degrees
    {
        // just return a value
        get => _degrees;
        private set
        {
            if (value < 73.15m)
            {
                throw new Exception("Ouch! It cannot be less than 73.15");
            }
            _degrees = value;
        }
    }
    
    public decimal CelsiusToFarenheit()=> (Degrees * (decimal)1.8) + 32;
    
    public decimal FarenheitToCelsius()
    {
        return ((Degrees - 32) * (decimal)0.5556);
    }
}


// Create a class named ExchangeRate with a constructor with two string parameters, fromCurrency and toCurrency. 
//Add a decimal property called Rate and method Calculate with decimal parameter amount return value of the method 
//should be a product of Rate and amount multiplication.
// Bonus: We should also check that Rate or amount are not negative.

public class ExchangeRate
{
    public decimal _rate;
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal Rate
    {
        get => _rate;
        set
        {
            if (value < 0)
            {
                throw new Exception("Rate can't be Negative");
            }
            _rate = value;
        }
    }
    public ExchangeRate(string fromCurrency, string toCurrency)
    {
        FromCurrency = fromCurrency;
        ToCurrency = toCurrency;
    }

    public decimal Calculate(decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception("Amount can't be Negative");
        }
        return Rate * amount;
    }
}

/*
Create interface IAnimal with property Name and Sound . 
Create classes Cow, Cat and Dog that implement IAnimal . 
Instantiate all three classes and pass them to a new method called MakeSound that has single parameter IAnimal 
and it writes to console eg “Dog says woof woof” if instance of the Dog class is passed.
*/
public interface IAnimal
{

    string Name { get; }
    string Sound { get; }
    void MakeSound();

}
public class Cow : IAnimal
{
    public string Name { get => "Cow"; }
    public string Sound { get => "Moo moo"; }
    public void MakeSound()
    {
        System.Console.WriteLine($"{Name} says {Sound}");
    }
}

public class Cat : IAnimal
{
    public string Name { get => "Cat"; }
    public string Sound { get => "Meow meow"; }
    public void MakeSound()
    {
        System.Console.WriteLine($"{Name} says {Sound}");
    }
}
public class Dog : IAnimal
{
    public string Name { get => "Dog"; }
    public string Sound { get => "Bhau bhau"; }
    public void MakeSound()
    {
        System.Console.WriteLine($"{Name} says {Sound}");
    }
}

/*
Create Account class that can be initialized with the amount value. 
Account class contains Withdraw and Deposit methods and Balance (get only) property. 
Make sure that you can't withdraw more than you have in the balance.
var account = new Account(100m);
account.Deposit(100);
Console.WriteLine($"Account balance is {account.Balance}");
account.Withdraw(20);
Console.WriteLine($"Account balance is {account.Balance}");
account.Withdraw(200); // ❌ we should not be able to withdraw more than we have in the balance
*/
public class Account
{
    private double _balance;
    public double Amount { get; set; }
    public double Balance { get=>_balance; }
  public Account(double amount)
  {
    this.Amount=amount;
  }  
  public double Withdraw(double withdrawAmount)
  {
    if(withdrawAmount>this._balance)
    {
        throw new Exception("Withdraw amount exceed current balance");
    }
    return this._balance=this._balance-withdrawAmount;
  }
  public double Deposit(double depositAmount)
  {
   return this._balance=this._balance+depositAmount;
    
  }
}


