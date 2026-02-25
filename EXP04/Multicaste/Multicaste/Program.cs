using System;

// Step 1: Declare delegate
delegate void Notify(string message);

class Program
{
    static void Main()
    {
        // Step 2: Create delegate instance
        Notify notifier = EmailAlert;
        notifier += SmsAlert;
        notifier += LogAlert;

        // Step 3: Call all methods at once
        notifier("Payment Successful");
    }

    static void EmailAlert(string msg)
    {
        Console.WriteLine("Email sent: " + msg);
    }

    static void SmsAlert(string msg)
    {
        Console.WriteLine("SMS sent: " + msg);
    }

    static void LogAlert(string msg)
    {
        Console.WriteLine("Logged in system: " + msg);
    }
}