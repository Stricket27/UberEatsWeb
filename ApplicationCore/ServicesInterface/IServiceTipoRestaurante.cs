using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterface
{
    public interface IServiceTipoRestaurante
    {
        IEnumerable<TipoRestaurante> ListaTipoRestaurantes();
    }
}
