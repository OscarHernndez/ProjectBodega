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
    public partial class FormTransacciones : Form
    {
        ProductosBL _productos;
        TransaccionesBL _transacciones;
        Contexto _contexto;
        public FormTransacciones()
        {
            InitializeComponent();
            _contexto = new Contexto();
            _transacciones = new TransaccionesBL();
            //listaProductosBindingSource.DataSource = _productos.ObtenerProductos();
            listaTransaccionesBindingSource.DataSource = _transacciones.ObtenerTransacciones();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var productoNombre = (textBox1.Text);
            _productos = new ProductosBL();
            listaProductosBindingSource.DataSource = _productos.ObtenerProductosPorDescripcion(productoNombre);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _transacciones.AgregarTransaccion();
            listaTransaccionesBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
            listaTransaccionesBindingNavigatorSaveItem.Enabled = true;
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;//Antepuesto(negacion, si es V es F, y si es F es V)
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
       /*     if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }
            }*/
        }

        private void listaTransaccionesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaTransaccionesBindingSource.EndEdit();
            var transaccion = (Transaccion)listaTransaccionesBindingSource.Current;

            var resultado = _transacciones.GuardarTransaccion(transaccion);
            var producto = _contexto.Productos.Find(transaccion.ProductoId);
            producto.Existencia = producto.Existencia - transaccion.Cantidad;
            _transacciones.ActualizarProducto(producto);

            if (resultado.Exitoso == true)
            {
                listaProductosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Datos Guardados");
                listaTransaccionesBindingNavigatorSaveItem.Enabled = false;
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }
    }
}
