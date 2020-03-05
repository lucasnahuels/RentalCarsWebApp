using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoCGB.Models;

namespace TrabajoCGB.Controllers
{
    public class CocheController : Controller
    {
        MantenimientoCoche MCoche = new MantenimientoCoche();
        MantenimientoOficina MO = new MantenimientoOficina();
//-----------------------------------------------------------------------------------------------      
        // GET: Coche
        public ActionResult ListadoCoches()
        {
            List<coche> coc = MCoche.RecuperarCoches();
            var orderedList = coc.OrderBy(c => c.marca);

            return View(orderedList);
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult ListadoOficinas()
        {
            return PartialView(MO.RecuperarOficinas());
        }
//-----------------------------------------------------------------------------------------------
        public static string RecuperarDireccionOficina(int codigo_unico_oficina)
        {
            MantenimientoOficina MO = new MantenimientoOficina();
            return MO.RecuperarDireccionOficina(codigo_unico_oficina);
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult AgregarCoche()
        {
            return View();
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult AgregarCoche(coche coc)
        {
            if(MO.RecuperarOficina(coc.codigo_unico_oficina) == null)
            {
                ModelState.AddModelError("", "Error. La oficina elegida no existe en la base de datos");
                return View();
            }

            int id_coche = -1;
            if (!MCoche.ExistePatenteCoche(coc.patente, id_coche))
            {
                ModelState.AddModelError("", "Error. El coche que quiere agregar ya existe en la base de datos");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    MCoche.Agregar(coc);
                    return RedirectToAction("ListadoCoches");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar el coche. " + ex.Message);
                    return View();              
                }
            }
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Editar(int id_coche)
        {
            return View(MCoche.RecuperarCoche(id_coche));
        }

//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(coche coc)
        {
            if (MO.RecuperarOficina(coc.codigo_unico_oficina) == null)
            {
                ModelState.AddModelError("", "Error. La oficina elegida no existe en la base de datos");
                return View(MCoche.RecuperarCoche(coc.id_coche));
            }
            if (!MCoche.ExistePatenteCoche(coc.patente, coc.id_coche))
            {
                ModelState.AddModelError("", "Error. El coche que quiere agregar ya existe en la base de datos");
                return View(MCoche.RecuperarCoche(coc.id_coche));
            }
            if (!ModelState.IsValid || MO.RecuperarOficina(coc.codigo_unico_oficina) == null)
            {
                return View(MCoche.RecuperarCoche(coc.id_coche));
            }
            else
            {
                try
                {
                    MCoche.Editar(coc);
                    return RedirectToAction("ListadoCoches");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al editar el coche. " + ex.Message);
                    return View();
                }
            }
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Eliminar(int id_coche)
        {
            return View(MCoche.RecuperarCoche(id_coche));
        }

//-----------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Borrar(int id_coche)
        {
            try
            {
                MCoche.Eliminar(id_coche);
                return RedirectToAction("ListadoCoches");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el coche. " + ex.Message);
                return RedirectToAction("Eliminar"); ;
            }
        }
    }
}