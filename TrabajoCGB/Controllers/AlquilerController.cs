using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoCGB.Models;

namespace TrabajoCGB.Controllers
{
    public class AlquilerController : Controller
    {
        MantenimientoAlquileres MA = new MantenimientoAlquileres();
        MantenimientoCoche MCoche = new MantenimientoCoche();
        MantenimientoConductor Mconductor = new MantenimientoConductor();
//-----------------------------------------------------------------------------------------------
        // GET: Alquiler
        public ActionResult ListadoAlquileres()
        {
            List<alquiler> alq = MA.RecuperarAlquileres();
            var orderedList = alq.OrderBy(a => a.id_conductor);
            return View(orderedList);
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult ListadoCoches()
        {
            return PartialView(MCoche.RecuperarCoches());
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult Agregar()
        {
            ViewData["no_existe_conductor"] = false; 
            return View();
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(alquiler alq, string DNI_conductor)
        {        
            if (MCoche.RecuperarCoche(alq.id_coche) == null)
            {           
                ViewData["no_existe_conductor"] = false;
              
                ModelState.AddModelError("", "Error. El coche elegido no existe en la base de datos");
                return View();
            }

            if (!ModelState.IsValid)
            {
                ViewData["no_existe_conductor"] = false;
                return View();
            }
            else
            {
                try
                {
                    int corroborar= Mconductor.RecuperarId_ConductorConDNI(int.Parse(DNI_conductor));
                    if (corroborar < 0)
                    {
                        ViewData["no_existe_conductor"] = true;
                        ViewData["dni"] = DNI_conductor;
                        return View();
                    }
                  
                    alq.id_conductor = corroborar;
                    MA.Agregar(alq);
                    return RedirectToAction("ListadoAlquileres");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar el alquiler. " + ex.Message);
                    return View();                 
                }
            }
        }

//-----------------------------------------------------------------------------------------------
        public ActionResult Editar(int id_alquiler)
        {
            ViewData["no_existe_conductor"] = false; 
            return View(MA.RecuperarAlquiler(id_alquiler));
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(alquiler alq, string DNI_conductor)
        {
            if (MCoche.RecuperarCoche(alq.id_coche) == null)
            {
              
                ViewData["no_existe_conductor"] = false;       
                ModelState.AddModelError("", "Error. El coche elegido no existe en la base de datos");
                return View(MA.RecuperarAlquiler(alq.id_alquiler));
            }
            if (!ModelState.IsValid)
            {
                ViewData["no_existe_conductor"] = false;       
                return View(MA.RecuperarAlquiler(alq.id_alquiler));
            }
            else
            {
                try
                {
                    int corroborar = Mconductor.RecuperarId_ConductorConDNI(int.Parse(DNI_conductor));
                    if (corroborar < 0)
                    {
                        ViewData["no_existe_conductor"] = true;
                        ViewData["dni"] = DNI_conductor;
                        return View();
                    }
                    alq.id_conductor = corroborar;
                    MA.Editar(alq);
                    return RedirectToAction("ListadoAlquileres");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al editar el alquiler. " + ex.Message);
                    return View();
                }
            }
        }

//-----------------------------------------------------------------------------------------------

        public ActionResult Eliminar(int id_alquiler)
        {
            return View(MA.RecuperarAlquiler(id_alquiler));
        }
//-----------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Borrar(int id_alquiler)
        {
            try
            {
                MA.Eliminar(id_alquiler);
                return RedirectToAction("ListadoAlquileres");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el alquiler. " + ex.Message);
                return RedirectToAction("Eliminar"); ;
            }
        }
    }
}