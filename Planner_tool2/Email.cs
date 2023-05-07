using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit;
using MailKit.Net.Imap;

namespace EmailToMySQL
{
    class Program
    {
        static void Email(string[] args)
        {
            // Set up connection parameters for the MySQL database
            string connString = "server=localhost;database=db_calendar;uid=root;sslmode=none";

            // Set up connection parameters for the email server
            string emailServer = "imap.gmail.com";
            int emailPort = 993;
            bool emailSsl = true;
            string emailUsername = "alexdbTest@gmail.com";
            string emailPassword = "Xr8KTqvWfY1fq2oOPvq6";

            // Connect to the MySQL database
            MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();

            // Connect to the email server using the IMAP protocol
            using (var client = new ImapClient())
            {
                client.Connect(emailServer, emailPort, emailSsl);

                // Authenticate using the email account username and password
                client.Authenticate(emailUsername, emailPassword);

                // Select the inbox folder
                client.Inbox.Open(FolderAccess.ReadOnly);

                // Iterate over each email message in the inbox
                for (int i = 0; i < client.Inbox.Count; i++)
                {
                    var message = client.Inbox.GetMessage(i);

                    // Extract the relevant data from the email message
                    string sender = message.From.ToString();
                    string subject = message.Subject;
                    string body = message.TextBody;

                    // Insert the extracted data into the MySQL database
                    String query = "INSERT INTO email_data (sender, subject, body) VALUES ('{sender}', '{subject}', '{body}')";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@subject", subject);
                        cmd.Parameters.AddWithValue("@body", body);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Disconnect from the email server
                client.Disconnect(true);
            }

            // Disconnect from the MySQL database
            connection.Close();
        }
    }
}