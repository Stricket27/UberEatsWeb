using ApplicationCore.Services;
using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UberEatsWeb.Controllers
{
    public class DireccionController : Controller
    {
        IServiceDireccion serviceDireccion = new ServiceDireccion();
        // GET: Direccion
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Direccion> listaDirecciones = null;
                listaDirecciones = serviceDireccion.ListaDirecciones();
                return View(listaDirecciones);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult AgregarDireccionView(Usuario usuario)
        {
            Usuario usuario1 = null;
            try
            {
                usuario1 = serviceDireccion.ObtenerUsuarioPorID(usuario.ID_Usuario);
                if (usuario1 != null)
                {
                    Session["Usuario"] = usuario1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AgregarDireccion(Direccion direccion)
        {
            try
            {
                if (direccion != null)
                {
                    serviceDireccion.AgregarDireccion(direccion);
                    TempData["NotificationMessage"] = "Se creo la dirección";
                    TempData["NotificationType"] = "success";
                }
                else
                {
                    TempData["NotificationMessage"] = "Hubo un error al crear la dirección";
                    TempData["NotificationType"] = "error";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditarDireccionView(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });

            ViewBag.listaEstado = listaEstado;

            Direccion direccion = null;
            try
            {
                direccion = serviceDireccion.ObtenerDireccionPorID(Convert.ToInt32(id));
                return View(direccion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        public ActionResult EditarDireccion(Direccion direccion)
        {
            try
            {
                if (direccion != null)
                {
                    Direccion oDireccion = serviceDireccion.EditarDireccion(direccion);
                    TempData["NotificationMessage"] = "La dirección fue editado";
                    TempData["NotificationType"] = "warning";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult CambiarEstado(int id)
        {
            try
            {
                TempData["NotificationMessage"] = "Se cambio el estado actual de la dirección";
                TempData["NotificationType"] = "info";
                serviceDireccion.CambiarEstado(id);
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
    }
}