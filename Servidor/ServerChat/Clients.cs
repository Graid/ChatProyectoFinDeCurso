using System;
using System.Collections.Generic;
using System.Net;

namespace ServerChat
{
    class Clients
    {
        private Dictionary<User, IPAddress> clients;

        public Clients()
        {
            clients = new Dictionary<User, IPAddress>();
        }

        public void newClient(User client, IPAddress clientIP)
        {
            clients.Add(client, clientIP);
        }

        public IPAddress getIP(User key)
        {
            return clients[key];
        }

        public Dictionary<User, IPAddress>.KeyCollection getKeys()
        {
            return clients.Keys;
        }

        public void printClients()
        {
            foreach (User key in clients.Keys)
            {
                Console.WriteLine("{0}: {1}", key.Name, clients[key]);
            }
        }
    }
}