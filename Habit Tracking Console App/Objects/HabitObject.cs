namespace Habit_Tracking_Console_App.Objects
{
    class HabitObject
    {
        private string name;
        private int importance;
        private bool isGood;
        private string description;
        private bool completed;
        private Frequency frequency;

        public HabitObject()
        {
            this.name = string.Empty;
            this.importance = 0;
            this.isGood = false;
            this.description = string.Empty;
            this.completed = false;
            this.frequency = new Frequency(FrequencyInterval.Daily, 1);
        }

        public HabitObject(string name, int importance, bool isGood, string description, Frequency? frequency = null)
        {
            this.name = name;
            this.importance = importance;
            this.isGood = isGood;
            this.description = description;
            if (frequency != null)
            {
                this.frequency = (Frequency)frequency;
            }
            else
            {
                this.frequency = new Frequency(FrequencyInterval.Daily, 1);
            }
            completed = false;
        }

        public void Edit(string? name = null, int? importance = null, bool? isGood = null, string? description = null, Frequency? frequency = null)
        {
            this.name = name ?? this.name;
            this.importance = importance ?? this.importance;
            this.isGood = isGood ?? this.isGood;
            this.description = description ?? this.description;
            this.frequency = frequency ?? this.frequency;
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

        public int Importance
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
