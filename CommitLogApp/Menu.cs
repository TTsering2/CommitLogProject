class Menu{
    public static void PrintMenu(){
        Console.WriteLine("***************************************************************************");
        Console.WriteLine("Hello! Welcome to my commit viewer app!");
        Console.WriteLine("***************************************************************************");
        Console.WriteLine("Here are the following commands you can run:");
        Console.WriteLine("1: Prompts the user credential menu");
        Console.WriteLine("2: Show your commit history for 24HR");
        Console.WriteLine("3: Shows your commit history for the last week");
        Console.WriteLine("4. Show your last previous query.");
        Console.WriteLine("9: Exit");
        Console.WriteLine("***************************************************************************");
        Console.WriteLine("Some helpful tips:");
        Console.WriteLine("-You can generate an access token through developer settings on Github.");
        Console.WriteLine("-Always ensure your token are private to just you!");
        Console.WriteLine("-Post commit query commands, you can hit enter again to load up the menu.");
        Console.WriteLine("-Keep coding!!!");
        Console.WriteLine("***************************************************************************");


    }
    

    public static int UserChoice(string input)
    {
        try
        {
            return int.Parse(input.Trim());  
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return -1;
        }
    }
}