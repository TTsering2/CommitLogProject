﻿namespace CommitLogApp;
using System;
using System.Security.AccessControl;

class Program
{
    static async void Main(string[] args){
       
        int userInput = 0;
        string accessToken = null;
        string username = null;
        string repository = null;

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
                        (accessToken, username, repository) =  userCredentials.PromptUser();

                    } catch (ArgumentException ex){
                        Console.WriteLine($"Input error: {ex.Message}");
                    } catch (Exception ex) {
                        Console.WriteLine($"An error has occured: {ex.Message}");
                    }
                    break;

                case 2:
                    Console.WriteLine("Here are your commits for the last 24 hours.");
                    var since24HoursAgo = DateTime.Now.AddDays(-1);
                    var timeNow = DateTime.Now;

                    try{
                        var commitService = new CommitService(accessToken);
                        await commitService.DisplayCommits(username, repository, since24HoursAgo, timeNow);
                    } catch (ArgumentException ex){
                        Console.WriteLine($"Input error: {ex.Message}");
                    } catch (Exception ex){
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    Console.ReadLine();
                    break;

                case 3:
                    Console.WriteLine("Here are your commits for the last week)");
                    var since7days = DateTime.Now.AddDays(-1);
                    timeNow = DateTime.Now;

                    try{
                        var commitService = new CommitService(accessToken);
                        await commitService.DisplayCommits(username, repository, since7days, timeNow);
                    } catch (ArgumentException ex){
                        Console.WriteLine($"Input error: {ex.Message}");
                    } catch (Exception ex){
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    Console.ReadLine();
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
