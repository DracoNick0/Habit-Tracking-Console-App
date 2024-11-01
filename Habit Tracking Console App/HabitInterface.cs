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

            this.habitManager.AddHabit(new HabitObject(name, isGood, description, importance));

            return true;
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
            string isGoodPrompt = "Is the habit a good habit? ";
            return InterfaceHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        private int PromptForImportance()
        {
            int? importance = null;

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the importance of this habit: ";
            while ((importance = InterfaceHelper.PromptForIntInput(importancePrompt)) == null || 1 > importance || importance > 5)
            {
                Console.Clear();
                Console.WriteLine("Input was not valid, try again!");
            }

            return (int)importance;
        }
    }
}
