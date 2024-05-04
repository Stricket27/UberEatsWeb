using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.RepositoryInterface
{
    public interface IRepositoryTipoRestaurante
    {
        IEnumerable<TipoRestaurante> ListaTipoRestaurantes();
    }
}
