﻿namespace Habit_Tracking_Console_App
{
    class InterfaceHelper
    {
        public static string PromptForNotNullInput(string prompt)
        {
            string? userInput = null;

            Console.Write(prompt);
            while ((userInput = Console.ReadLine()) == null)
            {
                Console.Clear();
                Console.WriteLine("Input was null, try again!");
                Console.Write(prompt);
            }

            return userInput;
        }

        public static string PromptForNotEmptyInput(string prompt)
        {
            string? userInput = null;

            Console.Write(prompt);
            while ((userInput = Console.ReadLine()) == string.Empty || userInput == null)
            {
                Console.Clear();
                Console.WriteLine("Input was empty, try again!");
                Console.Write(prompt);
            }

            return userInput;
        }

        public static bool PromptForTrueFalseInput(string prompt)
        {
            string? userInput = null;

            while (true)
            {
                userInput = PromptForNotEmptyInput(prompt);
                userInput = userInput.ToLower();

                switch (userInput)
                {
                    case string s when s == "true" || s == "y" || s == "yes":
                        Console.Clear();
                        return true;
                    case string s when s == "false" || s == "n" || s == "no":
                        Console.Clear();
                        return false;
                    default:
                        Console.WriteLine("Input was not valid, try again!");
                        break;
                }
            }
        }
    }
}
