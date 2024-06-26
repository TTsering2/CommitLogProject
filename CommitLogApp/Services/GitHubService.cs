namespace CommitLogApp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

public class GitHubService {
    private readonly GitHubClient _gitHubClient;

    public GitHubService(string accessToken){
        _gitHubClient = new GitHubClient(new ProductHeaderValue("GitReporter"))
        {
            Credentials = new Credentials(accessToken)
        };
    }

    public async Task<IReadOnlyList<GitHubCommit>> GetCommits(string username, string repository, DateTimeOffset since, DateTimeOffset until){
        try{
            var request = new CommitRequest
            {
                Since = since,
                Until = until
            };
            return await _gitHubClient.Repository.Commit.GetAll(username, repository, request);
        }
        catch (ApiException ex){
            Console.WriteLine($"GitHub API error: {ex.Message}");
            throw; 
        }
        catch (Exception ex){
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; 
        }
    }
}