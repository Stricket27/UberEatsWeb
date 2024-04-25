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
    public class RepositoryRestaurante : IRepositoryRestaurante
    {
        public IEnumerable<Restaurante> ListaRestaurantes()
        {
            try
            {
                IEnumerable<Restaurante> listaRestaurante = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaRestaurante = context.Restaurante.ToList();
                }
                return listaRestaurante;
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
        public void AgregarRestaurante(Restaurante restaurante)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Restaurante.Add(restaurante);
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
        public Restaurante EditarRestaurante(Restaurante restaurante)
        {
            int retorno = 0;
            Restaurante restaurante1 = null;
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    restaurante1 = ObtenerRestaurantePorID(restaurante.ID_Restaurante);

                    context.Restaurante.Add(restaurante);
                    context.Entry(restaurante).State = System.Data.Entity.EntityState.Modified;

                    retorno = context.SaveChanges();
                }
                if (retorno >= 0)
                {
                    restaurante1 = ObtenerRestaurantePorID(restaurante.ID_Restaurante);
                }
                return restaurante1;
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
                    context.Configuration.LazyLoadingEnabled = false;

                    Restaurante restaurante = (from r in context.Restaurante
                                               where r.ID_Restaurante == id
                                               select r).FirstOrDefault();

                    if (restaurante.EstadoActual == "Activo")
                    {
                        restaurante.EstadoActual = "Inactivo";
                    }
                    else if (restaurante.EstadoActual == "Activo")
                    {
                        restaurante.EstadoActual = "Activo";
                    }

                    context.Entry(restaurante).State = System.Data.Entity.EntityState.Modified;
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
        public Restaurante ObtenerRestaurantePorID(int id)
        {
            try
            {
                Restaurante restaurante = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    restaurante = context.Restaurante.Find(id);
                }
                return restaurante;
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
