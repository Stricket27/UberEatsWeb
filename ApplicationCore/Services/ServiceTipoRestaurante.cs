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
    public class ServiceTipoRestaurante : IServiceTipoRestaurante
    {
        IRepositoryTipoRestaurante repository = new RepositoryTipoRestaurante();
        public IEnumerable<TipoRestaurante> ListaTipoRestaurantes()
        {
            return repository.ListaTipoRestaurantes();
        }
    }
}
