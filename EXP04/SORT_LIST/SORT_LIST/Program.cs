using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 5, 2, 9, 1, 7 };

        numbers.Sort((a, b) => a.CompareTo(b));

        foreach (var num in numbers)
            Console.WriteLine(num);
    }
}