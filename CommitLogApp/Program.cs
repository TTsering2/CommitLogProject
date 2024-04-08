using System;
using System.IO;
using Newtonsoft.Json;

namespace CommitLogApp;

class Program{
    static void Main(string[] args){
        //global var we need to use throughout the switch block
        int userInput = 0;
        string accessToken = null;
        string username = null;
        string repository = null;
        DataService db = new DataService();


       //Main program
        while (userInput != 9){
            Menu.PrintMenu();
            Console.WriteLine("Please enter your choice:");
            string userInputString = Console.ReadLine();
            userInput = Menu.UserChoice(userInputString);
            switch (userInput){
                case 1:
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("1 selected");
                    Console.WriteLine("________________________________________________");
                    try{
                        var userCredentials = new UserCredentials();
                        (accessToken, username, repository) = userCredentials.PromptUser();
                    } catch (ArgumentException ex){
                        Console.WriteLine($"Input error: {ex.Message}");
                    } catch (Exception ex){
                        Console.WriteLine($"An error has occurred: {ex.Message}");
                    }
                    break;

                case 2:
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("Here are your commits for the last 24 hours.");
                    Console.WriteLine("________________________________________________");
                    var since24HoursAgo = DateTime.Now.AddDays(-1);
                    var timeNow = DateTime.Now;

                    Task.Run(async () =>{
                        try{
                            var commitService = new CommitService(accessToken);
                            await commitService.DisplayCommits(username, repository, since24HoursAgo, timeNow);
                            var lastRequest = new Data{
                                Username = username,
                                Repository = repository,
                                StartTime = since24HoursAgo,
                                EndTime = timeNow,
                            };
                            db.SaveRequestData(lastRequest);
                        }
                        catch (ArgumentException ex){
                            Console.WriteLine($"Input error: {ex.Message}");
                        }catch (Exception ex){
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        }).Wait(); 
                        Console.WriteLine("________________________________________________");
                        Console.ReadLine();
                        break;

                case 3:
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("Here are your commits for the last week.");
                    Console.WriteLine("________________________________________________");
                    var since7days = DateTime.Now.AddDays(-7);
                    timeNow = DateTime.Now;
                    Task.Run(async () =>{
                        try{
                            var commitService = new CommitService(accessToken);
                            await commitService.DisplayCommits(username, repository, since7days, timeNow);
                            var lastRequest = new Data{
                                Username = username,
                                Repository = repository,
                                StartTime = since7days,
                                EndTime = timeNow,
                            };
                            db.SaveRequestData(lastRequest);
                        }catch (ArgumentException ex){
                            Console.WriteLine($"Input error: {ex.Message}");
                        }catch (Exception ex){
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        }).Wait(); 
                        Console.WriteLine("________________________________________________");
                        Console.ReadLine();
                        break;

                case 4:
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("Printing last request!");
                    Console.WriteLine("________________________________________________");
                    
                    var lastRequest =  db.LoadLastRequest();

                    if (lastRequest != null){
                        Console.WriteLine($"Username: {lastRequest.Username}");
                        Console.WriteLine($"Repository: {lastRequest.Repository}");
                        Console.WriteLine($"Time Range: {lastRequest.StartTime} - {lastRequest.EndTime}");
                    }else{
                        Console.WriteLine("No previous request found.");
                    }
                    Console.WriteLine("________________________________________________");
                    Console.ReadLine();
                    break;

                case 9:
                    Console.WriteLine("Goodbye!");
                    Console.WriteLine("________________________________________________");
                    break;

                default:
                    Console.WriteLine("Invalid choice, please enter again!");
                    Console.WriteLine("________________________________________________");
                    break;
            }
        }
    }
}

