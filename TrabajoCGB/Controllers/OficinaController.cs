using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoCGB.Models;

namespace TrabajoCGB.Controllers
{

    public class OficinaController : Controller
    {
        MantenimientoOficina MO = new MantenimientoOficina();
        MantenimientoCiudad MC = new MantenimientoCiudad();

//-----------------------------------------------------------------------------------------------
        public ActionResult ListadoOficinas()
        {        
            List<OficinaAux> ofi = MO.CrearOficinaAux();
            var orderedList= ofi.OrderBy(o => o.id_ciudad);
        
            ViewData["nombreDeLaCiudad"] = " ";

            return View(orderedList);
        }

//-----------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult BuscarOficinas(string busquedaoficina)
        {
            List<OficinaAux> ofiAux = MO.CrearOficinaAux();
        
            string dato = busquedaoficina;
            ViewData["nombreDeLaCiudad"] = dato;

            return View("ListadoOficinas", ofiAux);
        }

//-----------------------------------------------------------------------------------------------
        public static string RecuperarNombreCiudad(int id_ciudad) 
        {
            MantenimientoCiudad MC = new MantenimientoCiudad(); 
            return MC.RecuperarNombreCiudad(id_ciudad);
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult ListaCiudades()
        {
            return PartialView(MC.ListaCiudades());
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Agregar()
        {
            ViewData["no_existe_ciudad"] = false;
            ViewData["nombre_ciudad"] = " "; 
            return View();
        }

//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Agregar(oficina Ofi, string nombre_ciudad)
        {         
            int cod = -1;
            if (!MO.ExisteDireccionOficina(Ofi.id_ciudad, Ofi.calle, Ofi.numero, cod ))
            {
                ViewData["no_existe_ciudad"] = false;
                ModelState.AddModelError("", "Error. La oficina que quiere agregar ya existe en la base de datos");
                return View();
            }

            if (!ModelState.IsValid)
            {            
                ViewData["no_existe_ciudad"] = false; 
                return View();
            }
            else
            {          
                try
                {
                    if(Ofi.id_ciudad<0)                 
                    {
                        ViewData["no_existe_ciudad"] = true;
                        ViewData["nombre_ciudad"] = nombre_ciudad;
                        return View();
                    }

                    MO.Agregar(Ofi);
                    return RedirectToAction("ListadoOficinas");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar la oficina. " + ex.Message);
                    return View();             
                }
            }
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Modificar(int cod)
        {
            ViewData["no_existe_ciudad"] = false; 
            ViewData["nombre_ciudad"] = " "; 
            return View(MO.RecuperarOficina(cod));
        }

//-----------------------------------------------------------------------------------------------  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modificar(oficina ofi, string nombre_ciudad)
        {
            if ((!MO.ExisteDireccionOficina(ofi.id_ciudad, ofi.calle, ofi.numero, ofi.codigo_unico_oficina)))
            {
                ViewData["no_existe_ciudad"] = false;
                ModelState.AddModelError("", "Error. La oficina que quiere agregar ya existe en la base de datos");
                return View(MO.RecuperarOficina(ofi.codigo_unico_oficina));
            }        
            if (!ModelState.IsValid)
            {
                ViewData["no_existe_ciudad"] = false;
                return View(MO.RecuperarOficina(ofi.codigo_unico_oficina));
            }
            else
            {
                try
                {
                    if (ofi.id_ciudad < 0)
                    {
                        ViewData["no_existe_ciudad"] = true;
                        ViewData["nombre_ciudad"] = nombre_ciudad;
                        return View(MO.RecuperarOficina(ofi.codigo_unico_oficina));
                    }

                    MO.Modificar(ofi);
                    return RedirectToAction("ListadoOficinas");
                }
                catch (Exception ex)
                {
                    ViewData["no_existe_ciudad"] = false;
                    ModelState.AddModelError("", "Error al editar la oficina. " + ex.Message);
                    return View();
                }
            }
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Eliminar(int cod)
        {
            return View(MO.RecuperarOficina(cod));
        }
//-----------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Borrar(int cod)
        {
            try
            {
                MO.Eliminar(cod);
                return RedirectToAction("ListadoOficinas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar la oficina. " + ex.Message);
                return RedirectToAction("Eliminar"); ;
            }
        }

    }
}
 