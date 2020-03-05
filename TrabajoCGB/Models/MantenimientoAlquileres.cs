using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabajoCGB.Models
{
    public class MantenimientoAlquileres
    {
        public List<alquiler> RecuperarAlquileres()
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                List<alquiler> alq = db.alquiler.ToList();
                return alq;
            }
        }

//-----------------------------------------------------------------------------------------------
        public alquiler RecuperarAlquiler(int id_alquiler)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                alquiler alq = db.alquiler.Find(id_alquiler);
                return alq;
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Agregar(alquiler alq)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                db.alquiler.Add(alq);
                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Editar(alquiler alq)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                alquiler alqAux = db.alquiler.Find(alq.id_alquiler);
                alqAux.id_alquiler = alq.id_alquiler;
                alqAux.precio = alq.precio;
                alqAux.tipo_seguro = alq.tipo_seguro;
                alqAux.duracion_dias = alq.duracion_dias;
                alqAux.id_coche = alq.id_coche;
                alqAux.id_conductor = alq.id_conductor;

                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Eliminar(int id_alquiler)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                alquiler alq = db.alquiler.Find(id_alquiler);
                db.alquiler.Remove(alq);
                db.SaveChanges();
            }
        }
    }
}