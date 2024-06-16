using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.RepositoryInterface
{
    public interface IReporsitoryDireccion
    {
        IEnumerable<Direccion> ListaDirecciones();
        Direccion ObtenerDireccionPorID(int id);
        Usuario ObtenerUsuarioPorID(int id);
        void AgregarDireccion(Direccion direccion);
        Direccion EditarDireccion(Direccion direccion);
        void CambiarEstado(int id);
        
    }
}
