using BL.Bodega;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Bodega
{
    public partial class FormLogin : Form
    {
        SeguridadBL _seguridad;

        public FormLogin()
        {
            InitializeComponent();

            _seguridad = new SeguridadBL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario;
            string contraseña;

            usuario = textBox1.Text;
            contraseña = textBox2.Text;

            button1.Enabled = false;
            button1.Text = "Verificando......";
            Application.DoEvents();

            var resultado = _seguridad.Autorizar(usuario, contraseña);

            if (resultado == true)
            {
                this.Close();
            }

            else
            { 

                MessageBox.Show("El usuario o la contraseña son incorrectas");
                button1.Text = "Aceptar";
                button1.Enabled = true;
            }
        }
    }
}
