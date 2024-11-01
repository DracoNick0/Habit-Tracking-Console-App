namespace Habit_Tracking_Console_App.Objects
{
    class HabitObject
    {
        private string name;
        private bool isGood;
        private string description;
        private int importance;
        private bool completed;

        public HabitObject()
        {
            name = string.Empty;
            isGood = true;
            description = string.Empty;
            importance = 0;
            completed = false;
        }

        public HabitObject(string name, bool isGood, string description, int importance)
        {
            this.name = name;
            this.isGood = isGood;
            this.description = description;
            this.importance = importance;
            completed = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsGood
        {
            get { return isGood; }
            set { isGood = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Imporatance
        {
            get { return importance; }
            set { importance = value; }
        }

        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

    }
}
