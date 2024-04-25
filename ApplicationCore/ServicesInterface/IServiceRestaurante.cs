using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterface
{
    public interface IServiceRestaurante
    {
        IEnumerable<Restaurante> ListaRestaurantes();
        Restaurante ObtenerRestaurantePorID(int id);
        void AgregarRestaurante(Restaurante restaurante);
        Restaurante EditarRestaurante(Restaurante restaurante);
        void CambiarEstado(int id);
    }
}
