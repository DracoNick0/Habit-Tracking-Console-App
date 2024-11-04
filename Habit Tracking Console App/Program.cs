using Habit_Tracking_Console_App.Model;
using Habit_Tracking_Console_App.Commander;
using Habit_Tracking_Console_App.PrintHelpers;

// Start window size monitoring.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();

TimeDependentVarUpdater updater = new TimeDependentVarUpdater();

CLIHelper.Msg("Hello World!");

// Run the main application.
CommandHandler commandHandler = new CommandHandler();
commandHandler.Run();

// Terminate process.
CLIHelper.Msg("See you again!");

