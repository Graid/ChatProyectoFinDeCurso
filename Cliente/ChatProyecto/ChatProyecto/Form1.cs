using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProyecto
{

    public partial class Form1 : Form
    {
        public Message message = new Message();
        public ClienteTCP conexion = new ClienteTCP();

        //Constructor
        public Form1(Message message, ClienteTCP conexion)
        {
            InitializeComponent();
            
            //Recoge la clase Message y ClienteTCP del formulario de conexion 
            this.message = message;
            this.conexion = conexion;

            //añade tu nombre de usuario a la lista
            label1.Text = message.Name;
       
        }
       

        //Muestra el dialogo para cambiar la fuente de letra
        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            enviarMensaje();
        }


        //Envia un mensaje al servidor
        private void enviarMensaje()
        {      
           String mensajeActual = txtMensaje.Text;

           //Mete en la clase Message el mensaje actual y le dice que el envio es un mensaja
           message.Data = mensajeActual;
           message.Code = 'M';

           //Envia el objeto con el metodo de la clase ClienteTCP 
           conexion.EnviarDatos(message);

           txtMensaje.Clear();
            
        }


        //Imprime el mensaje recibido en el area de chat
        private delegate void recibirMensajeItemDelegate(Message message);

        public void recibirMensaje(Message message)
        {
            if (this.txtChat.InvokeRequired)
            {
                this.txtChat.Invoke(new recibirMensajeItemDelegate(this.recibirMensaje), message);
            }
            else
            {
                txtChat.AppendText(message.Name + "> " + message.Data + "\n");
            }
        }

        //Cierra la aplicacion cuando se cierra el cliente
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
     
}
