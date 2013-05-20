using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerChat
{
    class ThreadSender
    {
        private Clients clientsList;
        private int port;
        private User message;

        public ThreadSender(Clients clientsList, User message, int port = 40404)
        {
            this.clientsList = clientsList;
            this.message = message;
            this.port = port;
        }

        public void runThreadSender()
        {
            Console.WriteLine("Sending data to clients");
            foreach (User key in clientsList.getKeys())
            {

                TcpClient client = null;
                StreamWriter writeStream = null;

                try
                {
                    Console.WriteLine("Sending data to" + message.Name);

                    client = new TcpClient();
                    IPEndPoint serverEndPoint = new IPEndPoint(clientsList.getIP(key), 194);
                    client.Connect(serverEndPoint);

                    writeStream = new StreamWriter(client.GetStream());

                    string obj = User.SerializeToString(new User(message.Name,'M',message.Message));

                    writeStream.Write(obj);

                    Console.WriteLine("Done");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Error while sending a stream.");
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    writeStream.Close();
                    client.Close();
                }
            }
        }
    }
}
