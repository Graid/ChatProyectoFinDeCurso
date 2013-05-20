using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace ChatProyecto
{
    public class ClienteTCP
    {
        private string IPservidor;
        NetworkStream clientStream;
        Byte[] data;
        TcpClient client;
        StreamReader readStream;


        public ClienteTCP(string IP)
        {
            IPservidor = IP;
        }

        public ClienteTCP() { }

        public Boolean conectarServidor()
        {
            try
            {
                client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(IPservidor), 194);
                client.Connect(serverEndPoint);
                clientStream = client.GetStream();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void cerrarServidor()
        {
            client.Close();
        }

        public void EnviarDatos(string mensaje)
        {
            clientStream = client.GetStream();
            data = System.Text.Encoding.ASCII.GetBytes(mensaje);
            clientStream.Write(data, 0, data.Length);
           
        }

        public void EnviarDatos(Message cliente)
        {
            //serializa la clase usuario para poder ser mandada a traves de TCP 
            StreamWriter writeStream = new StreamWriter(clientStream);
            string obj = Message.SerializeToString(cliente);

            //Envia el tamaño del objeto convertido en string
            writeStream.WriteLine(obj.Length);
            writeStream.Flush();

            //Envia el string
            writeStream.Write(obj);
            writeStream.Flush();
        }

        public static void RecibirDatos(Message message, Form1 form)
        {
            form.recibirMensaje(message);
        }


        public TcpClient Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        public NetworkStream ClientStream
        {
            get
            {
                return clientStream;
            }
            set
            {
                clientStream = value;
            }
        }

    }
}