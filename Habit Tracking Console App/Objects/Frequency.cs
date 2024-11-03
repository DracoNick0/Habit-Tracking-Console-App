namespace Habit_Tracking_Console_App.Objects
{
    public enum FrequencyInterval
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    struct Frequency
    {
        public FrequencyInterval interval;
        public int occurences;
    }
}
