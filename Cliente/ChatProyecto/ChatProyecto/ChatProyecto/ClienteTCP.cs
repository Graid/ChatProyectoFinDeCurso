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


namespace ChatProyecto
{
    public class ClienteTCP
    {
        private string IPservidor;
        NetworkStream clientStream;
        Byte[] data;
        TcpClient client;
        

        public ClienteTCP(string IP){
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
                return true;
            }
            catch(Exception e)
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
            clientStream.Close();
        }

        public void EnviarDatos(Usuario cliente)
        {
            //serializa la clase usuario para poder ser mandada a traves de TCP
            
          
            clientStream = client.GetStream();
          
            
            MemoryStream ms = new MemoryStream();
            IFormatter bf = new BinaryFormatter();           
            bf.Serialize(ms, cliente);
     
            //Coge el buffer de la clase serializada
            data = ms.GetBuffer();

            //Envia los datos al servidor
            clientStream.Write(data, 0, data.Length);
            clientStream.Flush();
            clientStream.Close();
            ms.Close();
        }


       

        public Usuario RecibirDatos()
        {

            clientStream = client.GetStream();
            
            data = new Byte[256];
            clientStream.Read(data , 0 , data.Length);
            clientStream.Close();

            //Recoge los datos
            MemoryStream ms2 = new MemoryStream(data);
            IFormatter bf2 = new BinaryFormatter();
            
            
            
            //crea el objeto Usuario y lo devuelve
            Usuario user = (Usuario)bf2.Deserialize(ms2);
            ms2.Close();
            return user;
        }
    }
}
