namespace Habit_Tracking_Console_App
{
    class TaskObject
    {
        private string name;
        private string description;
        private int importance;
        private DateTime startDate;
        private DateTime dueDate;
        private bool completed;

        public TaskObject()
        {
            this.name = string.Empty;
            this.description = string.Empty;
            this.importance = 0;
            this.startDate = DateTime.Now;
            this.dueDate = DateTime.Now;
            this.completed = false;
        }

        public string Name
        { 
            get { return this.name; }
            set { this.name = value; }
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

        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }

        public bool Completed
        { 
            get { return this.completed; }
            set { this.completed = value; }
        }

    }
}
