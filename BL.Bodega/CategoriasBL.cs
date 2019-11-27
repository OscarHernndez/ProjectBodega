using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bodega
{
   public class CategoriasBL
    {
        Contexto _contexto;

        public BindingList<Categoria> ListadeCategorias { get; set; }

        public CategoriasBL()
        {
            _contexto = new Contexto();
            ListadeCategorias = new BindingList<Categoria>();
        }

            public BindingList<Categoria> ObtenerCategorias()
        {
            _contexto.Categorias.Load();
            ListadeCategorias = _contexto.Categorias.Local.ToBindingList();

            return ListadeCategorias;
        }

    
    }

    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

    }
}
