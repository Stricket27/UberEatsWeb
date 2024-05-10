using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServicesInterface
{
    public interface IServiceTarjeta
    {
        IEnumerable<Tarjeta> ListaTarjetas();
        void AgregarTarjeta(Tarjeta tarjeta);
        void EliminarTarjeta(int id);
        List<TipoTarjeta> ListaTipoTarjeta();
        Tarjeta ObtenerTarjeraPorNumero(string numeroTarjeta);
        void CambiarEstado(int id);
    }
}
