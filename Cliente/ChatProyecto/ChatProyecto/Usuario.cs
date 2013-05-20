using System;

namespace ServerChat
{
    [Serializable]
    public class User
    {
        private string name;
        private string message;
        private char code;          // C = conexion, M = message, X = desconectar

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
    }
}