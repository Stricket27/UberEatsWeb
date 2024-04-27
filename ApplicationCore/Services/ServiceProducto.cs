using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        IRepositoryProducto repository = new RepositoryProducto();
        public void AgregarProducto(Producto producto)
        {
            repository.AgregarProducto(producto);
        }

        public void CambiarEstado(int id)
        {
            repository.CambiarEstado(id);
        }

        public Producto EditarProducto(Producto producto)
        {
            return repository.EditarProducto(producto);
        }

        public IEnumerable<Producto> listaProductos()
        {
            return repository.listaProductos();
        }

        public List<Restaurante> listaRestaurantes()
        {
            return repository.listaRestaurantes();
        }

        public List<TipoProducto> listaTipoProducto()
        {
            return repository.listaTipoProducto();
        }

        public Producto ObtenerProductoPorID(int id)
        {
            return repository.ObtenerProductoPorID(id);
        }
    }
}
