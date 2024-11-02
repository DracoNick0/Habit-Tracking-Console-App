using Habit_Tracking_Console_App.Backend;
using Habit_Tracking_Console_App.Interface;

// Monitor if the console is resized, then execute previous actions.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();

CommandHandler commandHandler = new CommandHandler();
string? userInput = string.Empty;

CLIHelper.Msg("Welcome back!");

do
{
    CLIHelper.Info("Enter \"help\" to print a list of commands.");
    CLIHelper.Prompt();

    // Wait for user input
    userInput = CLIHelper.ReadLine();

    // Execute the users input
} while (commandHandler.ExecuteCommand(userInput));

