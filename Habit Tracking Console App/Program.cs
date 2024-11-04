using Task_Tracking_Console_App.Backend.Logic.Commander;
using Task_Tracking_Console_App.Backend.Storage;
using Habit_Tracking_Console_App.Backend.Threads;
using Habit_Tracking_Console_App.Frontend;

// Start threads.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();
TimeDependentVarUpdater updater = new TimeDependentVarUpdater();

DynamicStorageManager dynamicStorageManager = new DynamicStorageManager();

IO.Msg("Hello World!");

// Run the main application.
CommandHandler commandHandler = new CommandHandler(dynamicStorageManager);
commandHandler.Run();

// Terminate process.
IO.Msg("See you again!");

