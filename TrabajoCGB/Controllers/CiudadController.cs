using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabajoCGB.Models;

namespace TrabajoCGB.Controllers
{
    public class CiudadController : Controller 
    {
        MantenimientoCiudad MCiu = new MantenimientoCiudad();
//-----------------------------------------------------------------------------------------------     
        // GET: Ciudad
        public ActionResult AgregarCiudad(string nombre_ciudad)
        {
            return View();
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult AgregarCiudad(ciudad ciu)
        {
            int id_ciudad = -1;
            if (!MCiu.ExisteNombreCiudad(ciu.nombre_ciudad, id_ciudad))
            {
                ModelState.AddModelError("", "Error. La ciudad que quiere agregar ya existe en la base de datos");
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
                    MCiu.Agregar(ciu);
                    return RedirectToAction("Detalles", null, new { id_ciudad = ciu.id_ciudad });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar la ciudad. " + ex.Message);
                    return View();                
                }
            }
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult Detalles(int id_ciudad)
        {
            return View(MCiu.RecuperarCiudad(id_ciudad));
        }
//-----------------------------------------------------------------------------------------------
        public ActionResult Editar(int id_ciudad)
        {
            return View(MCiu.RecuperarCiudad(id_ciudad));
        }
//-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ciudad ciu)
        {
            if (!MCiu.ExisteNombreCiudad(ciu.nombre_ciudad, ciu.id_ciudad))
            {
                ModelState.AddModelError("", "Error. La ciudad que quiere agregar ya existe en la base de datos");
                return View(MCiu.RecuperarCiudad(ciu.id_ciudad));
            }
            if (!ModelState.IsValid)
            {
                return View(MCiu.RecuperarCiudad(ciu.id_ciudad));
            }
            else
            {
                try
                {
                    MCiu.Editar(ciu);
                    return RedirectToAction("Detalles", null, new { id_ciudad = ciu.id_ciudad });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al editar la ciudad. " + ex.Message);
                    return View();
                }
            }
        }
    }
}