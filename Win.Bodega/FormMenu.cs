
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
    public partial class FormMenu : Form
    {

        public FormMenu()
        {
            InitializeComponent();

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            var formLogin = new FormLogin();
            formLogin.ShowDialog();
        }

        private void consultaDeProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formProducto = new FormProductos();
            formProducto.MdiParent = this;
            formProducto.Show();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            Login();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void salidasDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formTransaccion = new FormTransacciones();
            formTransaccion.MdiParent = this;
            formTransaccion.Show();
        }
    }
}
