using Habit_Tracking_Console_App.Model;
using Habit_Tracking_Console_App.View;
using Habit_Tracking_Console_App.ViewModel;

// Monitor if the console is resized, then execute previous actions.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();

MainCommandHandler commandHandler = new MainCommandHandler();
string? userInput = string.Empty;

CLIHelper.Msg("Hello World!");

do
{
    CLIHelper.Info("Enter \"help\" to print a list of commands.");
    CLIHelper.Prompt();

    // Wait for user input
    userInput = CLIHelper.ReadLine();

    // Execute the users input
} while (commandHandler.ExecuteCommand(userInput));

