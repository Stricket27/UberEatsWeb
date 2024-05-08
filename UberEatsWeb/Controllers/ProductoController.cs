using ApplicationCore.Services;
using ApplicationCore.ServicesInterface;
using Infraestructure.Models;
using Infraestructure.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UberEatsWeb.Controllers
{
    public class ProductoController : Controller
    {
        IServiceProducto serviceProducto = new ServiceProducto();
        // GET: Producto
        public ActionResult Index()
        {
            IEnumerable<Producto> listaProductos = null;
            try
            {
                listaProductos = serviceProducto.listaProductos();

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(listaProductos);
        }

        public ActionResult AgregarProductoView()
        {   //Tipo de producto
            var tipoProducto = TipoProductos().Select(x => new SelectListItem()
            {
                Text = x.TipoProducto1.ToString(),
                Value = x.ID_TipoProducto.ToString()
            }).ToList();

            tipoProducto.Insert(0, new SelectListItem
            {
                Text = "Seleccione el tipo de producto",
                Value = ""
            });

            ViewBag.TipoProducto = tipoProducto;

            //Restaurante
            var restaurante = Restaurantes().Select(x => new SelectListItem()
            {
                Text = x.Nombre.ToString(),
                Value = x.ID_Restaurante.ToString()
            }).ToList();

            restaurante.Insert(0, new SelectListItem
            {
                Text = "Seleccione el restaurante perteneciente",
                Value = ""
            });

            ViewBag.Restaurante = restaurante;

            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(HttpPostedFileBase File, Producto producto)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                if (File != null)
                {
                    string fotografia = Path.GetExtension(File.FileName);
                    File.InputStream.CopyTo(memoryStream);
                    producto.Fotografia = memoryStream.ToArray();
                    ModelState.Remove("Fotografia");
                }
                serviceProducto.AgregarProducto(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditarProductoView(int? id)
        {
            List<SelectListItem> listaEstado = new List<SelectListItem>();
            listaEstado.Add(new SelectListItem() { Text = "Activo", Value = "Activo" });
            listaEstado.Add(new SelectListItem() { Text = "Inactivo", Value = "Inactivo" });
            ViewBag.ListaEstado = listaEstado;

            ViewBag.TipoProducto = TipoProductos().Select(x => new SelectListItem
            {
                Text = x.TipoProducto1.ToString(),
                Value = x.ID_TipoProducto.ToString()
            }).ToList();

            ViewBag.Restaurante = Restaurantes().Select(x => new SelectListItem
            {
                Text = x.Nombre.ToString(),
                Value = x.ID_Restaurante.ToString()
            }).ToList();

            Producto producto = null;
            try
            {
                producto = serviceProducto.ObtenerProductoPorID(Convert.ToInt32(id));
                return View(producto);
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
        public ActionResult EditarProducto(HttpPostedFileBase File, Producto producto) {
            MemoryStream target = new MemoryStream();
            try
            {
                if (producto.Fotografia == null)
                {
                    if (File != null)
                    {
                        File.InputStream.CopyTo(target);
                        producto.Fotografia = target.ToArray();
                        ModelState.Remove("Fotografia");
                    }
                }

                Producto oProducto = serviceProducto.EditarProducto(producto);
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
                serviceProducto.CambiarEstado(id);
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

        public List<Restaurante> Restaurantes()
        {
            List<Restaurante> listaRestaurantes = null;
            try
            {

                listaRestaurantes = serviceProducto.listaRestaurantes();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaRestaurantes;
        }

        public List<TipoProducto> TipoProductos()
        {
            List<TipoProducto> listaTipoProducto = null;
            try
            {
                listaTipoProducto = serviceProducto.listaTipoProducto();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return listaTipoProducto;
        }
    }
}