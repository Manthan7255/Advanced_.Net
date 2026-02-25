using System;

class Program
{
    static void Main()
    {
        Action work = null;

        work += () => Console.WriteLine("Email Sent");
        work += () => Console.WriteLine("SMS Sent");
        work += () => Console.WriteLine("Push Notification Sent");

        work();

    }
}