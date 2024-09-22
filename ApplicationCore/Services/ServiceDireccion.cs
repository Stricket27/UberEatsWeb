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
    public class ServiceDireccion : IServiceDireccion
    {

        IReporsitoryDireccion reporsitoryDireccion = new RepositoryDireccion();

        public void AgregarDireccion(Direccion direccion)
        {
            reporsitoryDireccion.AgregarDireccion(direccion);
        }

        public void CambiarEstado(int id)
        {
            reporsitoryDireccion.CambiarEstado(id);
        }

        public Direccion EditarDireccion(Direccion direccion)
        {
            return reporsitoryDireccion.EditarDireccion(direccion);
        }

        public IEnumerable<Direccion> ListaDirecciones()
        {
            return reporsitoryDireccion.ListaDirecciones();
        }

        public Direccion ObtenerDireccionPorID(int id)
        {
            return reporsitoryDireccion.ObtenerDireccionPorID(id);
        }

        public Usuario ObtenerUsuarioPorID(int id)
        {
            return reporsitoryDireccion.ObtenerUsuarioPorID(id);
        }
    }
}
