using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Planner_tool2
{
    public partial class EventForm : Form
    {
       string connString = "server=localhost;database=db_calendar;uid=root;sslmode=none";



    private void button1_Click(object sender, EventArgs e)
{
   
}


        public EventForm()
        {
            InitializeComponent();
        }
        private void EventForm_Load(object sender, EventArgs e)
        {
            txdate.Text = Form1.static_year + "-" + Form1.static_month + "-" + UserControlDays.static_day;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            String sql = "INSERT INTO tbl_calendar(date,description,configuration,assigned,notes)values(?,?,?,?,?)";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("date", txdate.Text);
            cmd.Parameters.AddWithValue("description", txdesc.Text);
            cmd.Parameters.AddWithValue("configuration", txconfig.Text);
            cmd.Parameters.AddWithValue("assigned", txassign.Text);
            cmd.Parameters.AddWithValue("notes", txnote.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Saved");
            cmd.Dispose();
            conn.Close();
        }

        
    }
}
