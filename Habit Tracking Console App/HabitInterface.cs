using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Habit_Tracking_Console_App
{
    class HabitInterface
    {
        HabitManager habitManager;

        public HabitInterface()
        {
            this.habitManager = new HabitManager();
        }

        public bool HabitCreator()
        {
            string name, description;
            bool isGood;
            int importance;

            Console.WriteLine("You can make changes to the habit after answering the following prompts.");
            Console.Write("Enter the name of the habit: ");

            name = PromptForName();
            description = PromptForDescription();
            isGood = PromptForIsGood();
            importance = PromptForImportance();

            this.PromptForHabitCorrection(ref name, ref description, ref isGood, ref importance);

            this.habitManager.AddHabit(new HabitObject(name, isGood, description, importance));

            return true;
        }

        private bool PromptForHabitCorrection(ref string name, ref string description, ref bool isGood, ref int importance)
        {
            string? userInput;

            string prompt = "If a detail is incorrect, type it's name to change the property, otherwise press enter.\n";
            prompt += "Habit details:\n";
            prompt += " - Name: " + name + "\n";
            prompt += " - Desc: " + description + "\n";
            prompt += " - IsGood: " + isGood + "\n";
            prompt += " - Importance: " + importance + "\n";

            while (true)
            {
                userInput = InterfaceHelper.PromptForNotNullInput(prompt);
                userInput.ToLower();

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
                        return true;
                    default:
                        Console.Error.WriteLine("Input was not valid, try again!");
                        break;
                }
            }
        }

        private string PromptForName()
        {
            string namePrompt = "Enter the name of the habit: ";
            return InterfaceHelper.PromptForNotEmptyInput(namePrompt);
        }

        private string PromptForDescription()
        {
            string descriptionPrompt = "Enter the description of the habit: ";
            return InterfaceHelper.PromptForNotEmptyInput(descriptionPrompt);
        }

        private bool PromptForIsGood()
        {
            string isGoodPrompt = "Is this a good habit, enter true or false: ";
            return InterfaceHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        private int PromptForImportance()
        {
            int? importance = null;

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the habits importance: ";
            while ((importance = InterfaceHelper.PromptForIntInput(importancePrompt)) == null || 1 > importance || importance > 5)
            {
                Console.Clear();
                Console.WriteLine("Input was not valid, try again!");
            }

            return (int)importance;
        }
    }
}
