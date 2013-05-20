using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProyecto
{

    public partial class Form1 : Form
    {
        public Usuario user = new Usuario();
        public ClienteTCP conexion = new ClienteTCP();

        public Form1(Usuario user, ClienteTCP conexion)
        {
            InitializeComponent();
            this.user = user;
            this.conexion = conexion;
            label1.Text = user.getNombre();
        }
       
        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            txtMensaje.Font = fontDialog1.Font;
        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }

        private void enviarMensaje()
        {      
            String mensajeActual = txtMensaje.Text;
           

            user.setMensaje(mensajeActual);
            user.setCodigoMensaje('M');
            conexion.EnviarDatos(user);
            txtChat.AppendText(user.getNombre() + "> " + user.getMensaje());
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            enviarMensaje();
        }

        private void recibirMensaje()
        {
            Usuario user2 = conexion.RecibirDatos();
            if (user2.getCodigoMensaje() == 'M')
            {
                txtChat.AppendText(user2.getNombre() + "> " + user2.getMensaje());
            }

        }
    }
     
}
