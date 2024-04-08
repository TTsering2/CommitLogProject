using Xunit;
using Moq;
using System.IO;
using Newtonsoft.Json;

public class DataServiceTests{
    private const string TestFilePath = "./test.json"; 

    [Fact]
    public void SaveRequestData_Should_Write_Data_To_File(){
        var mockData = new Data { Username = "testuser", Repository = "testrepo", StartTime = DateTime.Now, EndTime = DateTime.Now };
        var mockFileWriter = new Mock<IFileWriter>();

        var dataService = new DataService(TestFilePath);

        dataService.SaveRequestData(mockData);


        Assert.True(File.Exists(TestFilePath)); // Check if file was created
        string jsonData = File.ReadAllText(TestFilePath);
        var deserializedData = JsonConvert.DeserializeObject<Data>(jsonData);
        Assert.NotNull(deserializedData);
        Assert.Equal(mockData.Username, deserializedData.Username);
        Assert.Equal(mockData.Repository, deserializedData.Repository);

    }

    [Fact]
    public void LoadLastRequest_Should_Return_Null_If_File_Not_Exists(){
        var dataService = new DataService(TestFilePath);

        var result = dataService.LoadLastRequest();

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
