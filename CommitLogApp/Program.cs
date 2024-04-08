namespace CommitLogApp;
using System;


class Program
{
    static  void Main(string[] args){
       
        int userInput = 0;

        while(userInput != 9){
            Menu.PrintMenu();
            Console.WriteLine("Please enter your choice:");
            string userInputString = Console.ReadLine();  
            userInput = Menu.UserChoice(userInputString);  

            switch(userInput){
                //Prompt user for the credentials
                case 1: 
                    Console.WriteLine("1 selected)");
                    try{
                        var userCredentials = new UserCredentials();
                        var (accessToken, username, repository) =  userCredentials.PromptUser();
                    } catch (ArgumentException ex){
                        Console.WriteLine($"Input error: {ex.Message}");
                    } catch (Exception ex) {
                        Console.WriteLine($"An error has occured: {ex.Message}");
                    }
                    break;

                case 2:
                    Console.WriteLine("2 selected)");
                    break;

                case 3:
                    Console.WriteLine("3 selected)");
                    break;

                case 9:
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice, please enter again!");
                    break;
            }
        }
    }
}
