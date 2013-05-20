using System;
using System.IO;
using System.Xml.Serialization;

namespace ServerChat
{
    [Serializable]
    public class User
    {
        private string name;
        private string message;
        private char code;          // C = conection M = message X = disconnection    <--- In
                                    // U = name used                                    <--- Out

        public User() { }

        public User(string name, char code, string message = null)
        {
            this.name = name;
            this.message = message;
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

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
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

        public static string SerializeToString(User user)
        {
            XmlSerializer serializer = new XmlSerializer(user.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, user);

                return writer.ToString();
            }
        }

        public static User DeserializeFromString(string userString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));

            User user;

            using (TextReader reader = new StringReader(userString))
            {   
                user = (User) serializer.Deserialize(reader);
                return user;
            }
        }
    }
}