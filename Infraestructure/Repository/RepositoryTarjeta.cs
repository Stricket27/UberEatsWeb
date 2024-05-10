using Infraestructure.Models;
using Infraestructure.RepositoryInterface;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTarjeta : IRepositoryTarjeta
    {
        public void AgregarTarjeta(Tarjeta tarjeta)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    tarjeta.NumeroTarjeta.ToLower();
                    tarjeta.EstadoActual = "Activo";
                    context.Tarjeta.Add(tarjeta);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void CambiarEstado(int id)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    Tarjeta tarjeta = (from t in context.Tarjeta
                                       where t.ID_Tarjeta == id
                                       select t).FirstOrDefault();

                    if (tarjeta.EstadoActual == "Activo")
                    {
                        tarjeta.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        tarjeta.EstadoActual = "Activo";
                    }

                    context.Entry(tarjeta).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void EliminarTarjeta(int id)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    Tarjeta tarjeta = context.Tarjeta.Find(id);
                    context.Tarjeta.Remove(tarjeta);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Tarjeta> ListaTarjetas()
        {
            try
            {
                IEnumerable<Tarjeta> listaTarjeta = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaTarjeta = context.Tarjeta.ToList();
                }
                return listaTarjeta;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public List<TipoTarjeta> ListaTipoTarjeta()
        {
            try
            {
                List<TipoTarjeta> listaTipoTarjeta = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaTipoTarjeta = context.TipoTarjeta.ToList();
                }
                return listaTipoTarjeta;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Tarjeta ObtenerTarjeraPorNumero(string numeroTarjeta)
        {
            try
            {
                Tarjeta tarjeta = null;
                using (MyContext context = new MyContext())
                {
                    tarjeta = (from t in context.Tarjeta
                               where t.NumeroTarjeta == numeroTarjeta
                               select t).FirstOrDefault();
                }
                return tarjeta;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
