using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bodega
{
    public class TransaccionesBL
    {
        Contexto _contexto;

        public BindingList<Transaccion> ListaTransacciones { get; set; }

        public TransaccionesBL()
        {
            _contexto = new Contexto();
            ListaTransacciones = new BindingList<Transaccion>();
        }

        public BindingList<Transaccion> ObtenerTransacciones()
        {
            _contexto.Transacciones.Load();
            ListaTransacciones = _contexto.Transacciones.Local.ToBindingList();

            return ListaTransacciones;
        }

        public BindingList<Transaccion> ObtenerTransaccionesPorProductos(int ProductoId)
        {
            _contexto.Transacciones.Where(t => t.ProductoId == ProductoId).Load();
            // var result = context.Table1.Where(c => c.UserCode == "123");
            //ListaTransacciones = _contexto.Transacciones.Local.ToBindingList().Where(t => t.ProductoId == ProductoId);
            //context.Employees.Where(e => e.IsActive).Load();
            //this.DataContext = context.Employees.Local;

            ListaTransacciones = _contexto.Transacciones.Local.ToBindingList();
            return ListaTransacciones;
        }

        public void AgregarTransaccion()
        {
            var nuevaTransaccion = new Transaccion();
            ListaTransacciones.Add(nuevaTransaccion);
        }

        public Resultado GuardarTransaccion(Transaccion transaccion)
        {
            

            var resultado = Validar(transaccion);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Exitoso = true;
            return resultado;
        }

        public void ActualizarProducto(Producto producto)
        {

            var productoexistente = _contexto.Productos.Find(producto.Id);
            productoexistente.Existencia = producto.Existencia;

            _contexto.SaveChanges();
        }

        private Resultado Validar(Transaccion transaccion)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (transaccion.ProductoId <= 0)
            {
                resultado.Mensaje = "Ingrese un codigo de Producto Valido";
                resultado.Exitoso = false;
            }

            if (transaccion.Cantidad <= 0)
            {
                resultado.Mensaje = "La Cantidad debe ser mayor a Cero";
                resultado.Exitoso = false;
            }


            return resultado;
        }


        public bool EliminarTransaccion(int id)
        {
            foreach (var producto in ListaTransacciones)
            {
                if (producto.Id == id)
                {
                    ListaTransacciones.Remove(producto);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

    }

    public class Transaccion
    {
        public int Id { get; set; }
        public string Ubicacion { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int TipoTransaccion { get; set; } //1=Ingreso 2=Egreso 3=Movimiento Interno
        public DateTime Fecha { get; set; }
        public int DocumentoId { get; set; } 
    }
}