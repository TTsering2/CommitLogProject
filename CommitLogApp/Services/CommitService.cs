using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;

public class CommitService{
    private GitHubService _gitHubService;

    public CommitService(string accessToken){
        _gitHubService = new GitHubService(accessToken);
    }

    public async Task DisplayCommits(string username, string repository, DateTimeOffset since, DateTimeOffset until){
        var commits = await _gitHubService.GetCommits(username, repository,since, until);

        Console.WriteLine($"Commits for {username}/{repository}:");
        foreach (var commit in commits)
        {
            Console.WriteLine($"- {commit.Commit.Author.Name} ({commit.Commit.Author.Date}): {commit.Commit.Message}");
        }
    }
}
