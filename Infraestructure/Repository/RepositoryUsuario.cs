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
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public void Agregar(Usuario usuario)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    usuario.CorreoElectronico.ToLower();
                    context.Usuario.Add(usuario);
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
                    Usuario usuario = (from u in context.Usuario
                                       where u.ID_Usuario == id
                                       select u).FirstOrDefault();

                    if (usuario.EstadoActual == "Activo")
                    {
                        usuario.EstadoActual = "Inactivo";
                    }
                    else
                    {
                        usuario.EstadoActual = "Activo";
                    }

                    context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
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

        public Usuario IniciarSesion(string correoElectronico, string contrasenna)
        {
            Usuario usuario = null;
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    usuario = (from u in context.Usuario
                               where u.CorreoElectronico == correoElectronico && u.Contrasenna == contrasenna
                               select u).FirstOrDefault<Usuario>();
                }
                if (usuario != null)
                {
                    usuario = ObtenerUsuarioPorID(usuario.ID_Usuario);
                    
                }
                return usuario;
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

        public List<Perfil> ListaPerfiles()
        {
            try
            {
                List<Perfil> listaPerfiles = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = true;
                    listaPerfiles = context.Perfil.ToList();
                }
                return listaPerfiles;
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

        public IEnumerable<Usuario> ListaUsuarios()
        {
            try
            {
                IEnumerable<Usuario> listaUsuario = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    listaUsuario = context.Usuario.ToList();
                }
                return listaUsuario;
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

        public Usuario ObtenerUsuarioPorCorreo(string correoElectronico)
        {
            try
            {
                Usuario usuario = null;
                using (MyContext context = new MyContext())
                {
                    usuario = (from u in context.Usuario
                               where u.CorreoElectronico == correoElectronico
                               select u).FirstOrDefault();
                }
                return usuario;
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

        public Usuario ObtenerUsuarioPorID(int id)
        {
            try
            {
                Usuario usuario = null;
                using (MyContext context = new MyContext())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    usuario = context.Usuario.Find(id);
                }
                return usuario;
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
