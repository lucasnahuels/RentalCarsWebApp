using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrabajoCGB.Models
{
    public class MantenimientoCiudad
    {
        public String RecuperarNombreCiudad(int id_ciudad)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                ciudad ciu1 = db.ciudad.Where(ciu => ciu.id_ciudad == id_ciudad).FirstOrDefault();
                if (ciu1 == null) return null;
                return ciu1.nombre_ciudad;
            }
        }
//-----------------------------------------------------------------------------------------------
        public List<ciudad> ListaCiudades()
        {
            using(var db= new BDDAlquilerAutosEntities())
            {
                return db.ciudad.ToList();
            }
        }

//-----------------------------------------------------------------------------------------------
        public void Agregar(ciudad ciu)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                db.ciudad.Add(ciu);
                db.SaveChanges();
            }
        }

//-----------------------------------------------------------------------------------------------
        public void Editar(ciudad ciu)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                ciudad ciuAux = db.ciudad.Find(ciu.id_ciudad);
                ciuAux.id_ciudad = ciu.id_ciudad;
                ciuAux.nombre_ciudad = ciu.nombre_ciudad;
                ciuAux.codigo_postal = ciu.codigo_postal;
   

                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public ciudad RecuperarCiudad(int id_ciudad)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                ciudad ciu= db.ciudad.Find(id_ciudad);
                return ciu;
            }
        }
//-----------------------------------------------------------------------------------------------
        public bool ExisteNombreCiudad(string nombre_ciudad, int id_ciudad)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                ciudad ciu = db.ciudad.Where(c => (c.nombre_ciudad == nombre_ciudad && c.id_ciudad != id_ciudad)).FirstOrDefault();
                if (ciu == null) return true;
                return false;
            }
        }

    }
}