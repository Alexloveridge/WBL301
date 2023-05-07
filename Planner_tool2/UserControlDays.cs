using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Planner_tool2
{
    public partial class UserControlDays : UserControl

    {

        string connString = "server=localhost;database=db_calendar;uid=root;sslmode=none";

        public static string static_day;
        private static DateTime VisibleDate;

        public UserControlDays()
        {
            InitializeComponent();
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }
        public void days(int numday)
        {
            lbdays.Text = numday+"";
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = lbdays.Text;
            timer1.Start();
            EventForm eventform = new EventForm();
            eventform.Show();
        }

        private void displayEvent()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            String sql = "SELECT * FROM tbl_calendar where date = ?";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", Form1.static_year + "-" + Form1.static_month + "-" +lbdays.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                lbdesc.Text = reader["description"].ToString();
            }
            reader.Dispose();
            cmd.Dispose();
            conn.Close();
        }

        private void DisplayEventsInCalendar()
        {
            // Get the visible range of dates in the calendar control
            DateTime start = UserControlDays.VisibleDate;
            DateTime end = start.AddMonths(1);

            // Retrieve events from the MySQL database that fall within the visible range
            List<CalendarEvent> events = GetEventsFromDatabase(start, end);

            // Add the events to the calendar control
            foreach (CalendarEvent evt in events)
            {
                UserControlDays.AddBoldedDate(evt.Date);
                UserControlDays.UpdateBoldedDates();

                // Add any additional properties to the calendar event
                UserControlDays.SetToolTip(evt.Date, evt.Description);
            }
        }

        private static void SetToolTip(object startDate, object title)
        {
            throw new NotImplementedException();
        }

        private static void UpdateBoldedDates()
        {
            throw new NotImplementedException();
        }

        private static void AddBoldedDate(object startDate)
        {
            throw new NotImplementedException();
        }

        private List<CalendarEvent> GetEventsFromDatabase(DateTime start, DateTime end)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Build the SQL query to retrieve events between the start and end dates
                string sql = "SELECT * FROM tbl_calendar WHERE date >= @start AND date < @end";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@start", start.Date);
                cmd.Parameters.AddWithValue("@end", end.Date);

                // Execute the query and retrieve the results
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new calendar event from the database row
                        CalendarEvent evt = new CalendarEvent
                        {
                            Id = reader.GetInt32("id"),
                            StartDate = reader.GetDateTime("date"),
                            Description = reader.GetString("description"),
                            Configuration = reader.GetString("configuration"),
                            Notes = reader.GetString("notes")
                        };

                        // Add the event to the list
                        events.Add(evt);
                    }
                }
            }

            return events;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            displayEvent();
        }
    }
}
