using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProyecto
{
    public partial class ConexionServer : Form
    {
        private Message message;
        private ClienteTCP conexion;

        public ConexionServer()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length != 0 && txtServidor.Text.Length != 0)
            {
                message = new Message(txtNombre.Text, 'C');
                conexion = new ClienteTCP(txtServidor.Text);
                if (conexion.conectarServidor())
                {
                    conexion.EnviarDatos(message);
                    Form1 chat = new Form1(message, conexion);
                    chat.Show();
                    this.Visible = false;
                    ThreadListener listener = new ThreadListener(conexion,chat);

                    Thread thLis = new Thread(new ThreadStart(listener.runThreadListener));
                    thLis.Start();
                }
                else
                {
                    MessageBox.Show("No se ha podido conectar con el servidor", "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se ha podido conectar con el servidor", "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
