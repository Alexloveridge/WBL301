namespace Planner_tool2
{
    internal class CalendarEvent
    {
        internal int Id;
        internal string Date;
        internal string Description;
        internal string Configuration;
        internal string Assigned;
        internal string Notes;

        public object StartDate { get; internal set; }
    }
}