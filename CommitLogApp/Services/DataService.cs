
using System;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CommitLogApp;

public class DataService{
    private const string filePath = "../db.json";

    public void SaveRequestData(Data data){
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, jsonData);
    }

    public Data LoadLastRequest(){
        if(File.Exists(filePath)){
            string jsonData = File.ReadAllText(filePath);
            if(!string.IsNullOrWhiteSpace(jsonData)){
                return JsonConvert.DeserializeObject<Data>(jsonData);
            }
        }
        return null;
    }
}