namespace Habit_Tracking_Console_App.Objects
{
    class HabitObject
    {
        private string name;
        private int importance;
        private bool isGood;
        private string description;
        private bool completed;
        private RecurrenceEnum recurrence;
        private int occurrence;

        public HabitObject()
        {
            this.name = string.Empty;
            this.importance = 0;
            this.isGood = false;
            this.description = string.Empty;
            this.completed = false;
            this.recurrence = RecurrenceEnum.Daily;
            this.occurrence = 1;
        }

        public HabitObject(string name, int importance, bool isGood, string description, RecurrenceEnum recurrence, int occurrence)
        {
            this.name = name;
            this.importance = importance;
            this.isGood = isGood;
            this.description = description;
            this.recurrence = recurrence;
            this.occurrence = occurrence;
            completed = false;
        }

        public void Edit(string? name = null, int? importance = null, bool? isGood = null, string? description = null, RecurrenceEnum? recurrence = null, int? occurrence = null)
        {
            this.name = name ?? this.name;
            this.importance = importance ?? this.importance;
            this.isGood = isGood ?? this.isGood;
            this.description = description ?? this.description;
            this.recurrence = recurrence ?? this.recurrence;
            this.occurrence = occurrence ?? this.occurrence;
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

        public RecurrenceEnum Recurrence
        {
            get { return recurrence; }
            set { recurrence = value; }
        }

        public int Occurrence
        {
            get { return occurrence; }
            set { occurrence = value; }
        }
    }
}
