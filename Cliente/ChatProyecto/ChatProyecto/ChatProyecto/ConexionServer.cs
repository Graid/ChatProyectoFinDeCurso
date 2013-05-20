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
    public partial class ConexionServer : Form
    {
        private Usuario user;
        private ClienteTCP conexion;

        public ConexionServer()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length != 0 && txtServidor.Text.Length != 0)
            {
                user = new Usuario(txtNombre.Text, 'c');
                conexion = new ClienteTCP(txtServidor.Text);
                if (conexion.conectarServidor())
                {
                    Form1 chat = new Form1(user, conexion);
                    chat.Show();
                    this.Visible = false;
                    conexion.cerrarServidor();
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
