namespace Habit_Tracking_Console_App
{
    class HabitObject
    {
        private string name;
        private bool isGood;
        private string description;
        private int importance;
        private DateTime endDate;
        private bool completed;

        public HabitObject()
        {
            this.name = string.Empty;
            this.isGood = true;
            this.description = string.Empty;
            this.importance = 0;
            this.endDate = DateTime.MaxValue;
            this.completed = false;
        }

        public HabitObject(string name, bool isGood, string description, int importance, DateTime endDate, bool completed)
        {
            this.name = name;
            this.isGood = isGood;
            this.description = description;
            this.importance = importance;
            this.endDate = endDate;
            this.completed = completed;
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

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public bool Completed
        { 
            get { return this.completed; }
            set { this.completed = value; }
        }

    }
}
