using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace ServerChat
{
    class ThreadListener
    {
        private Clients clientsList;
        private int port;

        public ThreadListener(Clients clientsList, int port = 194)
        {
            this.clientsList = clientsList;
            this.port = port;
        }

        public void runThreadListener()
        {
            TcpListener listener;

            Console.WriteLine("Conecting...");
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();

                Console.WriteLine("Connected");
                while (true)
                {
                    TcpClient client = null;
                    StreamReader readStream = null;

                    Console.WriteLine("Waiting for a client");

                    try
                    {
                        client = listener.AcceptTcpClient();

                        Console.WriteLine("New client");
                        readStream = new StreamReader(client.GetStream());

                        Console.WriteLine("Waiting for data");
                        string data = readStream.ReadToEnd();

                        User user = User.DeserializeFromString(data);

                        if (user.Code == 'M')
                        {
                            Console.WriteLine("End of stream");

                            Server.openSender(clientsList, user);
                        }
                        else
                        {
                            IPEndPoint ipEnd = (IPEndPoint)client.Client.RemoteEndPoint;
                            IPAddress ip = ipEnd.Address;

                            clientsList.newClient(user, ip);

                            Console.WriteLine("New conexion");
                            Console.WriteLine();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Clients List: ");
                            clientsList.printClients();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: Error while reading a stream.");
                        Console.WriteLine(e.StackTrace);
                    }
                    finally
                    {
                        readStream.Close();
                        client.Close();
                    }

                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("ERROR" + se.ErrorCode + ": Error while trying to start listener socket.");
                Environment.Exit(se.ErrorCode);
            }
        }
    }
}