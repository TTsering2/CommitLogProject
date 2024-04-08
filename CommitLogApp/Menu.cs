class Menu{
    public static void PrintMenu(){
        Console.WriteLine("Hello! Welcome to my commit viewer app!");
        Console.WriteLine("Here are the following commands you can run:");
        Console.WriteLine("1: Prompts the user credential menu");
        Console.WriteLine("2: Show your commit history for 24HR");
        Console.WriteLine("3: Shows your commit history for the last week");
        Console.WriteLine("4. Show your last previous query.");
        Console.WriteLine("9: Exit");
    }
    
    // public static int UserChoice(){
    //     try{
    //         Console.WriteLine($"this line: {Convert.ToInt32(Console.Read())}");
    //         Console.WriteLine($"console value: {Console.Read()}");
    //         return Convert.ToInt32(Console.Read());
    //     } catch (Exception ex){
    //         Console.WriteLine($"Your input was invalid. Please refer to the message and try again! Input Error: {ex.Message}");
    //         return -1;
    //     }
    // }
    public static int UserChoice(string input)
    {
        try
        {
            return int.Parse(input.Trim());  // Parse the input string into an integer
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return -1;
        }
    }
}