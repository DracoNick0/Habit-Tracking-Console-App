using Habit_Tracking_Console_App.Model;

namespace Habit_Tracking_Console_App.View
{
    /// <summary>
    /// Contains functions that prompt for interactions with habits.
    /// </summary>
    class HabitInterface
    {
        public HabitInterface() { }

        /// <summary>
        /// Displays all habits.
        /// </summary>
        /// <param name="displayIsGood">Determines if the isGood variable is displayed.</param>
        /// <param name="displayDescription">Determines if the description variable is displayed.</param>
        /// <param name="displayImportance">Determines if the importance variable is displayed.</param>
        /// <param name="displayCompletion">Determines if the completion variable is displayed.</param>
        public void DisplayAllHabits(List<HabitObject> habits, bool displayIsGood = true, bool displayImportance = true, bool displayCompletion = true, bool displayDescription = true)
        {
            if (habits.Count > 0)
            {
                string isGood = "", importance = "", completion = " ";
                foreach (HabitObject habit in habits)
                {
                    CLIHelper.MsgForWindow("+", "+", "+", '-');
                    if (displayCompletion)
                    {
                        if (habit.Completed)
                        {
                            completion = "x";
                        }
                    }

                    if (displayIsGood)
                    {
                        if (habit.IsGood)
                        {
                            isGood = "Positive Habit";
                        }
                        else
                        {
                            isGood = "Negative Habit";
                        }
                    }

                    if (displayImportance)
                    {
                        importance = habit.Importance.ToString();
                    }

                    CLIHelper.MsgForWindow($"| [{completion}] Habit: {habit.Name} ", "...|", "|");
                    CLIHelper.MsgForWindow($"| Importance: {importance} ", "...|", $"({isGood}) |");

                    if (displayDescription)
                    {
                        CLIHelper.MsgForWindow($"| Desc: {habit.Description} ", "...|", "|");
                    }
                }
            }
            else
            {
                CLIHelper.MsgForWindow("+", "+", "+", '-');
                CLIHelper.MsgForWindow($"| Empty", "...|", "|");
            }

            CLIHelper.MsgForWindow("+", "+", "+", '-');
        }

        /// <summary>
        /// Prompts the user for the name of a habit, then returns the corresponding habit.
        /// </summary>
        /// <returns>The habit object corresponding to the name provided.</returns>
        public string PromptForHabit()
        {
            return CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");
        }

        /// <summary>
        /// Prints prompts to allow the user to edit the habit object.
        /// </summary>
        /// <param name="name">Habits name.</param>
        /// <param name="importance">Habits importance.</param>
        /// <param name="isGood">Habits isGood.</param>
        /// <param name="description">Habits description</param>
        public void PromptForHabitCorrection(ref string name, ref int importance, ref bool isGood, ref string description)
        {
            string? userInput;

            while (true)
            {
                List<string> prompt = new List<string>();
                prompt.Add("(If a detail is incorrect, type it's name to change the property, otherwise press enter.)");
                prompt.Add("Habit details:");
                prompt.Add($"- Name: {name}");
                prompt.Add($"- Importance: {importance}");
                prompt.Add($"- IsGood: {isGood}");
                prompt.Add($"- Desc: {description}");

                userInput = CLIHelper.PromptForNotNullInput(prompt.ToArray()).ToLower();

                switch (userInput)
                {
                    case "name":
                        name = PromptForName();
                        break;
                    case "desc":
                        description = PromptForDescription();
                        break;
                    case "isgood":
                        isGood = PromptForIsGood();
                        break;
                    case "importance":
                        importance = PromptForImportance();
                        break;
                    case "":
                        return;
                    default:
                        CLIHelper.Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user for the name of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptForName()
        {
            string namePrompt = "Enter the name of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(namePrompt);
        }

        /// <summary>
        /// Prompts the user for the description of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptForDescription()
        {
            string descriptionPrompt = "Enter the description of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(descriptionPrompt);
        }

        /// <summary>
        /// Prompts the user for the polarity of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        public bool PromptForIsGood()
        {
            string isGoodPrompt = "Is this a good habit, enter true or false: ";
            return CLIHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        /// <summary>
        /// Prompts the user for the importance of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        public int PromptForImportance()
        {
            int? importance = null;

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the habits importance: ";
            while ((importance = CLIHelper.PromptForIntInput(importancePrompt)) == null || 1 > importance || importance > 5)
            {
                CLIHelper.Clear();
                CLIHelper.Error("Input was not valid, try again!");
            }

            return (int)importance;
        }
    }
}
