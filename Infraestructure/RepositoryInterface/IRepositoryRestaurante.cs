using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.RepositoryInterface
{
    public interface IRepositoryRestaurante
    {
        IEnumerable<Restaurante> ListaRestaurantes();
        Restaurante ObtenerRestaurantePorID(int id);
        void AgregarRestaurante(Restaurante restaurante);
        Restaurante EditarRestaurante(Restaurante restaurante);
        void CambiarEstado(int id);
    }
}
