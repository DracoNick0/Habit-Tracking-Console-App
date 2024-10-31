﻿using System.Runtime.CompilerServices;

namespace Habit_Tracking_Console_App
{
    class CommandHandler
    {
        HabitManager habitManager;
        TaskManager taskManager;

        public CommandHandler()
        {
            this.habitManager = new HabitManager();
            this.taskManager = new TaskManager();
        }

        public bool ExecuteCommand(string? userInput)
        {
            if (userInput != null)
            {
                // Change user input to be digestable by the program
                userInput = userInput.ToLower();
                string[] inputArgs = userInput.Split(' ');

                Console.Clear();
                switch (inputArgs[0])
                {
                    case "help": 
                        this.HelpCommand();
                        break;
                    case "add":
                        switch (inputArgs[1])
                        {
                            case "habit":
                                this.habitManager.AddHabit();
                                break;
                            case "task":
                                this.taskManager.AddTask();
                                break;
                            default: 
                                Console.Error.WriteLine("Cannot add \"" + inputArgs[1] + "\" does not exist, try again!");
                                break;
                        }
                        break;
                    case "exit":
                        this.ExitCommand();
                        return false;
                    default: 
                        Console.Error.WriteLine("The command \"" + userInput + "\" is not valid, try again!");
                        break;
                }
            }

            return true;
        }

        public void HelpCommand()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine(" - help: displays a list of commands for the user to input");
            Console.WriteLine(" - exit: exit the program");
        }

        public void ExitCommand()
        {
            Console.WriteLine("See you again!");
        }
    }
}
