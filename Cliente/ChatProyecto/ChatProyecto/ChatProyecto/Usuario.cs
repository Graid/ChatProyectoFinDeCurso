using System;


namespace ChatProyecto
{
    [Serializable] 
    public class Usuario
    {
        private string nombre;
        private string mensaje;
        private char codigoMensaje; // C = conexion, M = mensaje, X = desconectar

        public Usuario(string nombre, string mensaje, char codigoMensaje)
        {
            this.nombre = nombre;
            this.mensaje = mensaje;
            this.codigoMensaje = codigoMensaje;
        }

        public Usuario(string nombre, char codigoMensaje)
        {
            this.nombre = nombre;
            this.codigoMensaje = codigoMensaje;
        }

        public Usuario() { }

        public string getNombre()
        {
            return nombre;
        }

        public string getMensaje()
        {
            return mensaje;
        }

        public char getCodigoMensaje()
        {
            return codigoMensaje;
        }

        public void setMensaje(string mensaje) {
            this.mensaje = mensaje;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void setCodigoMensaje(char codigoMensaje)
        {
            this.codigoMensaje = codigoMensaje;
        }

    }
}
