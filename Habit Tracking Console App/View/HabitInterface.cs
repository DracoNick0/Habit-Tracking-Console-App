using Habit_Tracking_Console_App.Model;
using Habit_Tracking_Console_App.Model.Storage;

namespace Habit_Tracking_Console_App.View
{
    /// <summary>
    /// Contains functions that prompt for interactions with habits.
    /// </summary>
    class HabitInterface
    {
        private DynamicStorageManager dynamicStorage;

        public HabitInterface()
        {
            dynamicStorage = new DynamicStorageManager();
        }

        /// <summary>
        /// Displays all habits.
        /// </summary>
        /// <param name="displayIsGood">Determines if the isGood variable is displayed.</param>
        /// <param name="displayDescription">Determines if the description variable is displayed.</param>
        /// <param name="displayImportance">Determines if the importance variable is displayed.</param>
        /// <param name="displayCompletion">Determines if the completion variable is displayed.</param>
        public void DisplayAllHabits(bool displayIsGood = true, bool displayDescription = true, bool displayImportance = true, bool displayCompletion = true)
        {
            List<HabitObject> habits = dynamicStorage.getHabits();

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
                        importance = habit.Imporatance.ToString();
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

        public void PromptForHabitView()
        {
        }

        /// <summary>
        /// Prints prompts to allow the user to create a habit object.
        /// Calls to functions that create the babit and add it to the dynamic storage. ********************************************************************************************************
        /// </summary>
        public void PromptForHabitCreation()
        {
            string name, description;
            bool isGood;
            int importance;

            CLIHelper.Info("You can make changes to the habit after answering the following prompts.");

            name = PromptForName();
            description = PromptForDescription();
            isGood = PromptForIsGood();
            importance = PromptForImportance();

            HabitObject newHabit = new HabitObject(name, isGood, description, importance);

            PromptForHabitCorrection(newHabit);

            if (CLIHelper.PromptForTrueFalseInput("Save this habit?"))
            {
                this.dynamicStorage.Add(newHabit);
            }
        }

        /// <summary>
        /// Prompts the user for the name of a habit, then returns the corresponding habit.
        /// </summary>
        /// <returns>The habit object corresponding to the name provided.</returns>
        public HabitObject PromptForHabitObject()
        {
            string userInput;
            List<HabitObject> habits = dynamicStorage.getHabits();
            List<string> habitNames = habits.Select(habit => habit.Name).ToList();

            while (true)
            {
                DisplayAllHabits();

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                if (habitNames.Contains(userInput))
                {
                    return habits.First(habit => habit.Name == userInput);
                }
            }
        }

        /// <summary>
        /// Prompts the user for the name of a habit, then calls a function that removes the habit from the dynamic storage.
        /// </summary>
        public void PromptForHabitDelete()
        {
            dynamicStorage.RemoveHabit(PromptForHabitObject().Name);
        }

        /// <summary>
        /// Prompts the user for the name of a habit, then prints prompts to allow the user to edit the habit object.
        /// </summary>
        public void PromptForHabitEdit()
        {
            PromptForHabitCorrection(PromptForHabitObject());
        }

        /// <summary>
        /// Calls the habit manager to save the habits in the dynamic storage.
        /// Should not be called in the view, should be called in the view model *************************************************************************
        /// </summary>
        public void SaveHabits()
        {
            this.dynamicStorage.Save();
        }

        /// <summary>
        /// Prints prompts to allow the user to edit the habit object.
        /// </summary>
        /// <param name="habitObject">The habit object being edited.</param>
        private void PromptForHabitCorrection(HabitObject habitObject)
        {
            string? userInput;

            while (true)
            {
                List<string> prompt = new List<string>();
                prompt.Add("<If a detail is incorrect, type it's name to change the property, otherwise press enter.>");
                prompt.Add("Habit details:");
                prompt.Add($"- Name: {habitObject.Name}");
                prompt.Add($"- Desc: {habitObject.Description}");
                prompt.Add($"- IsGood: {habitObject.IsGood}");
                prompt.Add($"- Importance: {habitObject.Imporatance}");

                userInput = CLIHelper.PromptForNotNullInput(prompt.ToArray());
                userInput.ToLower();

                switch (userInput)
                {
                    case "name":
                        habitObject.Name = PromptForName();
                        break;
                    case "desc":
                        habitObject.Description = PromptForDescription();
                        break;
                    case "isgood":
                        habitObject.IsGood = PromptForIsGood();
                        break;
                    case "importance":
                        habitObject.Imporatance = PromptForImportance();
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
        private string PromptForName()
        {
            string namePrompt = "Enter the name of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(namePrompt);
        }

        /// <summary>
        /// Prompts the user for the description of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        private string PromptForDescription()
        {
            string descriptionPrompt = "Enter the description of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(descriptionPrompt);
        }

        /// <summary>
        /// Prompts the user for the polarity of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        private bool PromptForIsGood()
        {
            string isGoodPrompt = "Is this a good habit, enter true or false: ";
            return CLIHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        /// <summary>
        /// Prompts the user for the importance of a habit.
        /// </summary>
        /// <returns>User input.</returns>
        private int PromptForImportance()
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
