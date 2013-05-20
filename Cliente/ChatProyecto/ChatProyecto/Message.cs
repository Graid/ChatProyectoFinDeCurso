using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ChatProyecto
{
    [Serializable]
    public class Message 
    {
        private string name;
        private string data;
        private char code;          // C = conexion, M = message, X = desconectar

        public Message() { }

        public Message(string name, char code, string data = null)
        {
            this.name = name;
            this.data = data;
            this.code = code;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public char Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public static Message SerializeToObject(string obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Message));

            using (StringReader reader = new StringReader(obj))
            {
                Message message = (Message)serializer.Deserialize(reader);

                return message;
            }
        }
        public static string SerializeToString(Message message)
        {
            XmlSerializer serializer = new XmlSerializer(message.GetType());
            XmlWriterSettings xml = new XmlWriterSettings();
            xml.NewLineHandling = NewLineHandling.Entitize;

            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter writerXml = XmlWriter.Create(writer, xml))
                {
                    serializer.Serialize(writerXml, message);

                    return writer.ToString();
                }
            }

            
        }

        //public static string SerializeToString(Message Message)
        //{
        //    XmlSerializer serializer = new XmlSerializer(Message.GetType());

        //    using (StringWriter writer = new StringWriter())
        //    {
        //        serializer.Serialize(writer, Message);

        //        return writer.ToString();
        //    }
        //}
    }
}