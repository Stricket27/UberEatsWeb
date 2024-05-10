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
    public class ServiceTarjeta : IServiceTarjeta
    {
        IRepositoryTarjeta repositoryTarjeta = new RepositoryTarjeta();
        public void AgregarTarjeta(Tarjeta tarjeta)
        {
            repositoryTarjeta.AgregarTarjeta(tarjeta);
        }

        public void CambiarEstado(int id)
        {
            repositoryTarjeta.CambiarEstado(id);
        }

        public void EliminarTarjeta(int id)
        {
            repositoryTarjeta.EliminarTarjeta(id);
        }

        public IEnumerable<Tarjeta> ListaTarjetas()
        {
            return repositoryTarjeta.ListaTarjetas();
        }

        public List<TipoTarjeta> ListaTipoTarjeta()
        {
            return repositoryTarjeta.ListaTipoTarjeta();
        }

        public Tarjeta ObtenerTarjeraPorNumero(string numeroTarjeta)
        {
            return repositoryTarjeta.ObtenerTarjeraPorNumero(numeroTarjeta);
        }
    }
}
