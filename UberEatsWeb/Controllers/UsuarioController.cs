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
        public ActionResult RegistrarUsuarioView()
        {

            var perfil = Perfil().Select(x => new SelectListItem
            {
                Text = x.Perfil1.ToString(),
                Value = x.ID_Perfil.ToString()
            }).ToList();

            var usuarioID = 3;

            var perfilPorDefecto = perfil.FirstOrDefault(x => x.Value == usuarioID.ToString());
            if (perfilPorDefecto != null)
            {
                perfilPorDefecto.Selected = true;
            }

            ViewBag.Perfil = perfil;

            return View();
        }
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
        public ActionResult AgregarUsuarioView()
        {
            var perfilLista = Perfil().Select(x => new SelectListItem()
            {
                Text = x.Perfil1.ToString(),
                Value = x.ID_Perfil.ToString()
            }).ToList();

            perfilLista.Insert(0, new SelectListItem
            {
                Text = "Seleccione el perfil",
                Value = ""
            });

            ViewBag.Perfil = perfilLista;

            return View();
        }


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

        public ActionResult EditarUsuarioView(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.ListaEstado = listaEstado;

            ViewBag.Perfil = Perfil().Select(x => new SelectListItem
            {
                Text = x.Perfil1.ToString(),
                Value = x.ID_Perfil.ToString()
            }).ToList();

            ServiceUsuario service = new ServiceUsuario();
            Usuario usuario = null;
            try
            {
                usuario = service.ObtenerUsuarioPorID(Convert.ToInt32(id));
                return View(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }


        }

        [HttpPost]
        public ActionResult EditarUsuario(Usuario usuario)
        {
            try
            {
                Usuario usuario1 = serviceUsuario.EditarUsuario(usuario);
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
        public ActionResult IniciarSesionView()
        {
            return View();
        }

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
                    usuario = serviceUsuario.IniciarSesion(pUsuario.CorreoElectronico, pUsuario.Contrasenna);
                    if (usuario != null)
                    {
                        Session["Usuario"] = usuario;
                        Session["Rol"] = usuario.Perfil;
                        Log.Info($"Inicio sesion: {pUsuario.CorreoElectronico}");
                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Bienvenido",
                            usuario.Nombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido, 
                            Util.SweetAlertHelper.SweetAlertMessageType.success
                            );
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio: {pUsuario.CorreoElectronico}");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("¿Quién eres?",
                            "Esta cuenta es inválida, intente de nuevo", Util.SweetAlertHelper.SweetAlertMessageType.error
                            );
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