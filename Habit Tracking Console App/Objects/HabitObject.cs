namespace Habit_Tracking_Console_App.Objects
{
    class HabitObject
    {
        private string name;
        private int importance;
        private bool isGood;
        private string description;
        private bool completed;

        public HabitObject(string name, int importance, bool isGood, string description)
        {
            this.name = name;
            this.importance = importance;
            this.isGood = isGood;
            this.description = description;
            completed = false;
        }

        public void Edit(string? name = null, int? importance = null, bool? isGood = null, string? description = null)
        {
            this.name = name ?? this.name;
            this.importance = importance ?? this.importance;
            this.isGood = isGood ?? this.isGood;
            this.description = description ?? this.description;
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
