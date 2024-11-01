using Habit_Tracking_Console_App.Backend;
using Habit_Tracking_Console_App.Interface;

CommandHandler commandHandler = new CommandHandler();
string? userInput = string.Empty;
CLIHelper.Message("Welcome back!");

do
{
    CLIHelper.Info("Enter \"help\" to print a list of commands.");
    CLIHelper.Prompt();

    // Wait for user input
    userInput = Console.ReadLine();

    // Execute the users input
} while (commandHandler.ExecuteCommand(userInput));