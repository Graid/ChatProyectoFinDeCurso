using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace ServerChat
{
    class Server
    {
        private static Clients clients = new Clients();

        public static void Main(string[] args)
        {
            Console.Clear();

            openListener(clients);

            while (true)
            {
                Console.Read();
            }
        }

        public static void openListener(Clients clients)
        {
            ThreadListener listener = new ThreadListener(clients);

            Thread thLis = new Thread(new ThreadStart(listener.runThreadListener));
            thLis.Start();
        }

        public static void openSender(Clients clients, User user)
        {
                ThreadSender sender = new ThreadSender(clients , user);

                Thread thLis = new Thread(new ThreadStart(sender.runThreadSender));
                thLis.Start();
        }
    } 
}