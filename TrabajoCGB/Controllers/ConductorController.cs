using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoCGB.Models;

namespace TrabajoCGB.Controllers
{
    public class ConductorController : Controller //seria bueno crear un proceso automatico que elimine los conductores automaticamente dos años despues de su regsitro para asi limpiar la base de datos constantemente
    {
        MantenimientoConductor MConductor = new MantenimientoConductor();
        // GET: Conductor
//-----------------------------------------------------------------------------------------------
        public ActionResult ListadoConductores()
        {
            List<conductor> con = MConductor.RecuperarConductores();
            var orderedList = con.OrderBy(c => c.DNI);

            return View(orderedList);
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult AgregarConductor(string DNI)
        {
            return View();
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult AgregarConductor(conductor con)
        {
            int id_conductor = -1;
            if (!MConductor.ExisteDNIConductor(con.DNI, id_conductor))
            {
                ModelState.AddModelError("", "Error. El conductor que quiere agregar ya existe en la base de datos");
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
                    MConductor.Agregar(con);
                    return RedirectToAction("Detalles", null, new { id_conductor = con.id_conductor });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar el conductor. " + ex.Message);
                    return View();               
                }
            }
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult Detalles(int id_conductor)
        {
            return View(MConductor.RecuperarConductor(id_conductor));
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult Editar(int id_conductor)
        {
            return View(MConductor.RecuperarConductor(id_conductor));
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(conductor con)
        {
            if (!MConductor.ExisteDNIConductor(con.DNI, con.id_conductor))
            {
                ModelState.AddModelError("", "Error. El conductor que quiere agregar ya existe en la base de datos");
                return View(MConductor.RecuperarConductor(con.id_conductor));
            }
            if (!ModelState.IsValid)
            {
                return View(MConductor.RecuperarConductor(con.id_conductor));
            }
            else
            {
                try
                {
                    MConductor.Editar(con);
                    return RedirectToAction("Detalles", null, new { id_conductor = con.id_conductor });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al edita el conductor. " + ex.Message);
                    return View();
                }
            }
        }
//-----------------------------------------------------------------------------------------------
        public static int RecuperarDNI_conductor(int id_conductor)
        {
            MantenimientoConductor MConductor = new MantenimientoConductor();
            return MConductor.RecuperarDNIConductor(id_conductor);
        }

        public ActionResult Eliminar(int id_conductor)
        {
            return View(MConductor.RecuperarConductor(id_conductor));
        }

//-----------------------------------------------------------------------------------------------
        [HttpGet]   
        public ActionResult Borrar(int id_conductor)
        {
            try
            {
                MConductor.Eliminar(id_conductor);
                return RedirectToAction("ListadoConductores");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el conductor. " + ex.Message);
                return RedirectToAction("Eliminar"); ;
            }
        }
    }
}