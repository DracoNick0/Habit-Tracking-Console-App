namespace Habit_Tracking_Console_App
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
            this.name = string.Empty;
            this.isGood = true;
            this.description = string.Empty;
            this.importance = 0;
            this.completed = false;
        }

        public HabitObject(string name, bool isGood, string description, int importance)
        {
            this.name = name;
            this.isGood = isGood;
            this.description = description;
            this.importance = importance;
            this.completed = false;
        }

        public string Name
        { 
            get { return this.name; }
            set { this.name = value; }
        }

        public bool IsGood
        {
            get { return this.isGood; }
            set { this.isGood = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Imporatance
        {
            get { return this.importance; }
            set { this.importance = value; }
        }

        public bool Completed
        { 
            get { return this.completed; }
            set { this.completed = value; }
        }

    }
}
