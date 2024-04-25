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
    public class ServiceRestaurante : IServiceRestaurante
    {
        IRepositoryRestaurante repository = new RepositoryRestaurante();
        public void AgregarRestaurante(Restaurante restaurante)
        {
            repository.AgregarRestaurante(restaurante);
        }

        public void CambiarEstado(int id)
        {
            repository.CambiarEstado(id);
        }

        public Restaurante EditarRestaurante(Restaurante restaurante)
        {
            return repository.EditarRestaurante(restaurante);
        }

        public IEnumerable<Restaurante> ListaRestaurantes()
        {
            return repository.ListaRestaurantes();
        }

        public Restaurante ObtenerRestaurantePorID(int id)
        {
            return repository.ObtenerRestaurantePorID(id);
        }
    }
}
