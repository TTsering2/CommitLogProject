using Xunit;
using System.IO;
using Newtonsoft.Json;
using CommitLogApp;

public class DataServiceTests
{
    private const string TestFilePath = "./test.json"; 

    [Fact]
    public void SaveRequestData_Should_Write_Data_To_File()
    {
        var mockData = new Data { Username = "testuser", Repository = "testrepo", StartTime = DateTime.Now, EndTime = DateTime.Now };

        var dataService = new DataService(TestFilePath);

        dataService.SaveRequestData(mockData);

        Assert.True(File.Exists(TestFilePath)); 
        string jsonData = File.ReadAllText(TestFilePath);
        var deserializedData = JsonConvert.DeserializeObject<Data>(jsonData);
        Assert.NotNull(deserializedData);
        Assert.Equal(mockData.Username, deserializedData.Username);
        Assert.Equal(mockData.Repository, deserializedData.Repository);
    }

    [Fact]
    public void LoadLastRequest_Should_Return_Null_If_File_Not_Exists()
    {
        // Arrange
        var dataService = new DataService(TestFilePath);

        // Ensure the file does not exist by deleting it if it exists
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }

        // Act
        var result = dataService.LoadLastRequest();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void LoadLastRequest_Should_Return_Deserialized_Data_If_File_Exists_And_Not_Empty()
    {
        var mockData = new Data { Username = "testuser", Repository = "testrepo", StartTime = DateTime.Now, EndTime = DateTime.Now };
        File.WriteAllText(TestFilePath, JsonConvert.SerializeObject(mockData));

        var dataService = new DataService(TestFilePath);

        var result = dataService.LoadLastRequest();

        Assert.NotNull(result);
        Assert.Equal(mockData.Username, result.Username);
        Assert.Equal(mockData.Repository, result.Repository);
    }
}
