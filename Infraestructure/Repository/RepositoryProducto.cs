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
    public class RepositoryProducto : IRepositoryProducto
    {
        public void AgregarProducto(Producto producto)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Producto.Add(producto);
                    producto.EstadoActual = "Activo";
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
                    context.Configuration.LazyLoadingEnabled = false;

                    Producto producto = (from p in context.Producto
                                               where p.ID_Producto == id
                                               select p).FirstOrDefault();

                    if (producto.EstadoActual == "Activo")
                    {
                        producto.EstadoActual = "Inactivo";
                    }
                    else if (producto.EstadoActual == "Inactivo")
                    {
                        producto.EstadoActual = "Activo";
                    }

                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
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

        public Producto EditarProducto(Producto producto)
        {
            int retorno = 0;
            Producto producto1 = null;
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    producto1 = ObtenerProductoPorID(producto.ID_Producto);
                    context.Producto.Add(producto);

                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    retorno = context.SaveChanges();

                    if (retorno >= 0)
                    {
                        producto1 = ObtenerProductoPorID(producto.ID_Producto);
                    }
                    return producto1;
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

        public IEnumerable<Producto> listaProductos()
        {
            try
            {
                IEnumerable<Producto> listaProductos = null;

                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaProductos = context.Producto.ToList();
                }
                return listaProductos;
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

        public List<Restaurante> listaRestaurantes()
        {
            try
            {
                List<Restaurante> listaRestaurantes = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaRestaurantes = context.Restaurante.ToList();
                }
                return listaRestaurantes;
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

        public List<TipoProducto> listaTipoProducto()
        {
            try
            {
                List<TipoProducto> listaTipoProducto = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaTipoProducto = context.TipoProducto.ToList();
                }
                return listaTipoProducto;
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

        public Producto ObtenerProductoPorID(int id)
        {
            try
            {
                Producto producto = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    producto = context.Producto.Find(id);
                }
                return producto;
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
