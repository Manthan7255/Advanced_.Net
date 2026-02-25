using System;

// Step 1: Declare delegate
delegate double Operation(double a, double b);

class Calculator
{
    // Step 2: Define methods
    static double Add(double x, double y) => x + y;
    static double Subtract(double x, double y) => x - y;
    static double Multiply(double x, double y) => x * y;
    static double Divide(double x, double y)
    {
        if (y == 0)
        {
            Console.WriteLine("Cannot divide by zero!");
            return 0;
        }
        return x / y;
    }

    static void Main()
    {
        Console.Write("Enter first number: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter second number: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("\nChoose operation:");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Subtract");
        Console.WriteLine("3. Multiply");
        Console.WriteLine("4. Divide");

        Console.Write("Enter choice (1-4): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        // Step 3: Assign delegate based on choice
        Operation op = choice switch
        {
            1 => Add,
            2 => Subtract,
            3 => Multiply,
            4 => Divide,
        };

        if (op == null)
        {
            Console.WriteLine("Invalid choice!");
            return;
        }

        // Step 4: Call delegate
        double result = op(a, b);
        Console.WriteLine($"\nResult = {result}");
    }
}