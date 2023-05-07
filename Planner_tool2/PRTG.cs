using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Threading;

//public class SmtpListener
//{
//    public static void Main()
//    {
//       // Set up a listener for incoming SMTP notifications
//     var listener = new SmtpListener(IPAddress.Any, 25
//   listener.Start();

// while (true)
//        {
//            // Wait for incoming messages
//            var client = listener.AcceptSmtpClient();
//           var message = MailMessage.LoadFromSmtpMessage(client);

            // Process the message
//            ProcessMessage(message);

            // Close the connection
//            client.Close();
//        }
//    }

//