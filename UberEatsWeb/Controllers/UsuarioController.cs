using ApplicationCore.Services;
using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UberEatsWeb.Utils;
using static UberEatsWeb.Util.SweetAlertHelper;

namespace UberEatsWeb.Controllers
{
    public class UsuarioController : Controller
    {
        IServiceUsuario serviceUsuario = new ServiceUsuario();
        // Se va a mostrar la lista de todos los Usuario, este metodo esta por default y es mejor no cambiar el nombre de Index
        public ActionResult Index()
        {
            IEnumerable<Usuario> listaUsuarios = null;
            try
            {
                listaUsuarios = serviceUsuario.ListaUsuarios();
                if (TempData.ContainsKey("NotificationMessage"))
                {
                    ViewBag.NotificationMessage = TempData["NotificationMessage"];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
            return View(listaUsuarios);
        }

        //Carga de datos cuando el usuario se va a registrar=
        public ActionResult RegistrarUsuarioView()
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            ViewBag.ListaEstado = listaEstado;

            ViewBag.Perfil = Perfil().Select(x => new SelectListItem
            {
                Text = x.Perfil1.ToString(),
                Value = x.ID_Perfil.ToString()
            }).ToList();

            return View();
        }

        //Acción que el usuario pueda registrarse
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            try
            {
                if (serviceUsuario.ObtenerUsuarioPorCorreo(usuario.CorreoElectronico.ToLower()) != null)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("ERROR",
                        "No se puede agregar otro usuario con el mismo correo electrónico:" + " " +
                        usuario.CorreoElectronico,
                        SweetAlertMessageType.error);
                }
                else
                {
                    serviceUsuario.AgregarUsuario(usuario);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
            return RedirectToAction("IniciarSesionView");
        }

        //Carga de datos cuando el Administrador va agregar un usuario nuevo
        public ActionResult AgregarUsuarioView()
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            ViewBag.ListaEstado = listaEstado;

            ViewBag.Perfil = Perfil().Select(x => new SelectListItem
            {
                Text = x.Perfil1.ToString(),
                Value = x.ID_Perfil.ToString()
            }).ToList();

            return View();
        }

        //Acción que realizar cuando el Administrador ya agregó un nuevo usuario

        [HttpPost]
        public ActionResult AgregarUsuario(Usuario usuario)
        {
            try
            {
                if (serviceUsuario.ObtenerUsuarioPorCorreo(usuario.CorreoElectronico.ToLower()) != null)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("ERROR",
                        "No se puede agregar otro usuario con el mismo correo electrónico:" + " " +
                        usuario.CorreoElectronico,
                        SweetAlertMessageType.error);
                }
                else
                {
                    serviceUsuario.AgregarUsuario(usuario);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
            return RedirectToAction("Index");
        }

        //Lista que muestra todos los perfiles disponibles en la DB
        public List<Perfil> Perfil()
        {
            List<Perfil> listaPerfiles = null;
            try
            {
                listaPerfiles = serviceUsuario.ListaPerfiles();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaPerfiles;
        }

        //Acción que va a realizar al cambiar el estado actual
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                serviceUsuario.CambiarEstado(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
            return RedirectToAction("Index");
        }

        //Metodo en donde se va a redigir en iniciar sesión
        public ActionResult IniciarSesionView()
        {
            return View();
        }

        //Metodo para iniciar sesión
        [HttpPost]
        public ActionResult IniciarSesion(Usuario pUsuario)
        {
            Usuario usuario = null;
            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("PrimerApellido");
                ModelState.Remove("SegundoApellido");
                ModelState.Remove("ID_Perfil");

                if (ModelState.IsValid)
                {
                    usuario = serviceUsuario.IniciarSesion(pUsuario.CorreoElectronico, pUsuario.NombreUsuario, pUsuario.Contrasenna);
                    if (usuario != null)
                    {
                        Session["Usuario"] = usuario;
                        Session["Rol"] = usuario.Perfil;
                        Log.Info($"Inicio sesion: {pUsuario.CorreoElectronico}");
                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Bienvenido",
                            usuario.Nombre + " " + usuario.PrimerApellido, SweetAlertMessageType.success
                            );
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View("IniciarSesionView");
        }

        //Metodo por si no tiene autorizacion de un sitio
        public ActionResult SinAutorizacion()
        {
            ViewBag.Message = "No autorizado";
            if (Session["Usuario"] != null)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                Log.Warn($"No autorizado {usuario.CorreoElectronico}");
            }
            return View();
        }

        //Metodo para cerrar sesión
        public ActionResult CerrarSesion()
        {
            try
            {
                Session["Usuario"] = null;
                Session.Remove("Usuario");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }







    }
}