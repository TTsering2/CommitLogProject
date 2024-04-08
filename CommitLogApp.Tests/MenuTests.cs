namespace CommitLogApp.Tests;
using System;
using System.IO;
using Xunit;


public class MenuTests{
    [Fact]
    public void PrintMenu_Should_Print_Expected_Output(){
        using (var consoleOutput = new ConsoleOutput()){
            Menu.PrintMenu();

            // Assert
            string output = consoleOutput.GetOutput();
            Assert.Contains("Welcome to my commit viewer app!", output);
            Assert.Contains("1: Prompts the user credential menu", output);
            Assert.Contains("2: Show your commit history for 24HR", output);
            Assert.Contains("3: Shows your commit history for the last week", output);
            Assert.Contains("4. Show your last previous query", output);
            Assert.Contains("9: Exit", output);
            Assert.Contains("Some helpful tips:", output);
        }
    }


    [Theory]
    [InlineData("1", 1)]         // "1" should return 1
    [InlineData("2", 2)]         //  "2" should return 2
    [InlineData("9", 9)]         // "9" should return 9
    [InlineData("invalid", -1)]  // "invalid" should return -1
    [InlineData("", -1)]         // Empty input: should return -1
    public void UserChoice_Should_Parse_Input_Correctly(string input, int expectedChoice){
      
        int actualChoice = Menu.UserChoice(input);

        Assert.Equal(expectedChoice, actualChoice);
    }

    [Fact]
    public void UserChoice_Should_Display_Error_Message_For_Invalid_Input() {

        using (var consoleOutput = new ConsoleOutput()){
            int actualChoice = Menu.UserChoice("invalid");

            Assert.Equal(-1, actualChoice); 
            string output = consoleOutput.GetOutput();
            Assert.Contains("Invalid input. Please enter a valid number.", output); // Verify error message
        }
    }
}


public class ConsoleOutput : IDisposable{
    private StringWriter _stringWriter;
    private TextWriter _originalOutput;

    public ConsoleOutput(){
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOutput(){
        return _stringWriter.ToString();
    }

    public void Dispose(){
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}
