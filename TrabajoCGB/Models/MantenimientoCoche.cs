using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabajoCGB.Models
{
    public class MantenimientoCoche
    {
        public List<coche> RecuperarCoches()
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                List<coche> coc = db.coche.ToList();
                return coc;
            }
        }
//-----------------------------------------------------------------------------------------------
        public static string RecuperarPatenteCoche(int id_coche)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                return db.coche.Find(id_coche).patente;
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Agregar(coche coc)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                db.coche.Add(coc);
                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public coche RecuperarCoche(int id_coche)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                coche coc = db.coche.Find(id_coche);
                return coc;
            }
        }

//-----------------------------------------------------------------------------------------------
        public void Editar(coche coc)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                coche cocAux = db.coche.Find(coc.id_coche);
                cocAux.id_coche= coc.id_coche;
                cocAux.grupo = coc.grupo;
                cocAux.marca = coc.marca;
                cocAux.modelo = coc.modelo;
                cocAux.numero_pasajeros = coc.numero_pasajeros;
                cocAux.numero_puertas = coc.numero_puertas;
                cocAux.capacidad_baúl = coc.capacidad_baúl;
                cocAux.patente = coc.patente;
                cocAux.codigo_unico_oficina = coc.codigo_unico_oficina;
                
                db.SaveChanges();
            }
        }

//-----------------------------------------------------------------------------------------------
        public void Eliminar(int id_coche)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                coche coc = db.coche.Find(id_coche);
                db.coche.Remove(coc);
                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public bool ExistePatenteCoche(string patente, int id_coche)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                coche coc = db.coche.Where(c => (c.patente == patente && c.id_coche != id_coche)).FirstOrDefault();
                if (coc == null) return true;
                return false;
            }
        }
    }
}