namespace CommitLogApp;


public class UserCredentials {
    public (string accessToken, string username, string repository) PromptUser(){
        Console.Write("Enter your GitHub Access Token: ");
        string accessToken =  Console.ReadLine();

        Console.Write("Enter yourGithub username: ");
        string username = Console.ReadLine();

        Console.Write("Enter the name of your repository: ");
        string repository = Console.ReadLine();

        //Ensuring non-empty inputs
        if(string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(repository)){
            throw new ArgumentException("One or more of your inputs are empty");
        }
        return (accessToken, username, repository);
    }
}