namespace Habit_Tracking_Console_App
{
    class HabitObject
    {
        private string habit;
        private bool isGood;
        private string description;
        private int importance;
        private DateTime endDate;
        private bool completed;

        public HabitObject()
        {
            this.habit = string.Empty;
            this.isGood = true;
            this.description = string.Empty;
            this.importance = 0;
            this.endDate = DateTime.MaxValue;
            this.completed = false;
        }

        public string Habit
        { 
            get { return this.habit; }
            set { this.habit = value; }
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
