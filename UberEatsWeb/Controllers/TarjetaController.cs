using ApplicationCore.Services;
using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UberEatsWeb.Controllers
{
    public class TarjetaController : Controller
    {
        IServiceTarjeta serviceTarjeta = new ServiceTarjeta();
        // GET: Tarjeta
        public ActionResult Index()
        {
            IEnumerable<Tarjeta> listaTarjeta = null;
            try
            {
                listaTarjeta = serviceTarjeta.ListaTarjetas();
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
            return View(listaTarjeta);
        }

        public ActionResult AgregarTarjetaView()
        {
            ViewBag.TipoTarjeta = TipoTarjeta().Select(x => new SelectListItem
            {
                Text = x.TipoTarjeta1.ToString(),
                Value = x.ID_TipoTarjeta.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarTarjeta(Tarjeta tarjeta)
        {
            try
            {
                if (serviceTarjeta.ObtenerTarjeraPorNumero(tarjeta.NumeroTarjeta.ToLower()) != null)
                {
                    TempData["NotificationMessage"] = "No se puede agregar otra tarjeta con el mismo número de tarjeta";
                    TempData["NotificationType"] = "error";
                }
                else
                {
                    serviceTarjeta.AgregarTarjeta(tarjeta);
                    TempData["NotificationMessage"] = "La tarjeta se creo exitosamente";
                    TempData["NotificationType"] = "success";
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

        public ActionResult CambiarEstado(int id)
        {
            try
            {
                serviceTarjeta.CambiarEstado(id);
                TempData["NotificationMessage"] = "Se cambio el estado actual de la tarjeta";
                TempData["NotificationType"] = "info";
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

        public ActionResult EliminarTarjeta(int id)
        {
            try
            {
                serviceTarjeta.EliminarTarjeta(id);
                TempData["NotificationMessage"] = "Se eliminó la tarjeta";
                TempData["NotificationType"] = "info";
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

        public List<TipoTarjeta> TipoTarjeta()
        {
            List<TipoTarjeta> listaTipoTarjeta = null;
            try
            {
                listaTipoTarjeta = serviceTarjeta.ListaTipoTarjeta();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaTipoTarjeta;
        }
    }
}