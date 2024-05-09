using ApplicationCore.Services;
using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UberEatsWeb.Controllers
{
    public class RestauranteController : Controller
    {
        IServiceRestaurante serviceRestaurante = new ServiceRestaurante();
        // GET: Restaurante
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Restaurante> listaRestaurante = null;
                listaRestaurante = serviceRestaurante.ListaRestaurantes();
                return View(listaRestaurante);
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

        public ActionResult AgregarRestauranteView(Usuario usuario)
        {
            var tipoRestaurantesList = TipoRestaurantes().Select(x => new SelectListItem()
            {
                Text = x.TipoRestaurante1.ToString(),
                Value = x.ID_TipoRestaurante.ToString()
            }).ToList();

            tipoRestaurantesList.Insert(0, new SelectListItem
            {
                Text = "Seleccione el tipo de restaurante",
                Value = ""
            });

            ViewBag.TipoRestaurante = tipoRestaurantesList;

            //ViewBag.TipoRestaurante = ListaTipoRestaurantes();

            Usuario usuario1 = null;
            try
            {
                usuario1 = serviceRestaurante.ObtenerUsuarioPorID(usuario.ID_Usuario);
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
        public ActionResult AgregarRestaurante(HttpPostedFileBase File, Restaurante restaurante)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                if (restaurante != null)
                {
                    if (File != null)
                    {
                        string fotografia = Path.GetExtension(File.FileName);
                        File.InputStream.CopyTo(memoryStream);
                        restaurante.Fotografia = memoryStream.ToArray();
                        ModelState.Remove("Fotografia");
                    }
                    serviceRestaurante.AgregarRestaurante(restaurante);
                    TempData["NotificationMessage"] = "Se creo el restaurante";
                    TempData["NotificationType"] = "success";
                }
                else
                {
                    TempData["NotificationMessage"] = "Hubo un error al crear el restaurante";
                    TempData["NotificationType"] = "error";
                }
                
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditarRestauranteView(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });

            ViewBag.listaEstado = listaEstado;

            ViewBag.TipoRestaurante = TipoRestaurantes().Select(x => new SelectListItem
            {
                Text = x.TipoRestaurante1.ToString(),
                Value = x.ID_TipoRestaurante.ToString()
            }).ToList();

            //ServiceRestaurante service = new ServiceRestaurante();
            Restaurante restaurante = null;
            try
            {
                restaurante = serviceRestaurante.ObtenerRestaurantePorID(Convert.ToInt32(id));
                return View(restaurante);
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
        public ActionResult EditarRestaurante(HttpPostedFileBase File, Restaurante restaurante)
        {
            MemoryStream target = new MemoryStream();
            try
            {
                if (restaurante.Fotografia == null)
                {
                    if (File != null)
                    {
                        File.InputStream.CopyTo(target);
                        restaurante.Fotografia = target.ToArray();
                        ModelState.Remove("Fotografia");
                    }
                }
                if (restaurante != null)
                {
                    Restaurante oRestaurante = serviceRestaurante.EditarRestaurante(restaurante);
                    TempData["NotificationMessage"] = "El restaurante fue editado";
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

        public ActionResult DetalleRestauranteView(int? id)
        {
            try
            {
                Restaurante restaurante = null;

                restaurante = serviceRestaurante.ObtenerRestaurantePorID(Convert.ToInt32(id));

                return View(restaurante);
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
                TempData["NotificationMessage"] = "Se cambio el estado actual del restaurante";
                TempData["NotificationType"] = "info";
                serviceRestaurante.CambiarEstado(id);
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

        public List<TipoRestaurante> TipoRestaurantes()
        {
            List<TipoRestaurante> listaRestaurante = null;
            try
            {
                listaRestaurante = serviceRestaurante.TipoRestaurantes();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaRestaurante;
        }

        //public MultiSelectList ListaTipoRestaurantes(ICollection<TipoRestaurante> tipoRestaurantes = null)
        //{
        //    IServiceTipoRestaurante serviceTipoRestaurante = new ServiceTipoRestaurante();
        //    IEnumerable<TipoRestaurante> listaTipoRestauarante = serviceTipoRestaurante.ListaTipoRestaurantes();
        //    int[] listaTR = null;
        //    if (tipoRestaurantes != null)
        //    {
        //        listaTR = tipoRestaurantes.Select(tr => tr.ID_TipoRestaurante).ToArray();
        //    }
        //    return new MultiSelectList(listaTipoRestauarante, "ID_TipoRestaurante", "TipoRestaurante1", listaTR);
        //}
    }
}