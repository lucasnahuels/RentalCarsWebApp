using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabajoCGB.Models
{
    public class MantenimientoConductor
    {
        public List<conductor> RecuperarConductores()
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                List<conductor> con = db.conductor.ToList();
                return con;
            }
        }

//-----------------------------------------------------------------------------------------------
        public int RecuperarDNIConductor(int id_conductor)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                return db.conductor.Find(id_conductor).DNI;
            }
        }
//-----------------------------------------------------------------------------------------------
        public int RecuperarId_ConductorConDNI(int DNI_condcutor)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                conductor c1= db.conductor.Where(c => c.DNI == DNI_condcutor).FirstOrDefault();
                if (c1 == null) return -1;
                return c1.id_conductor;
            }        
        }
//-----------------------------------------------------------------------------------------------
        public void Agregar(conductor con)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                db.conductor.Add(con);
                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public conductor RecuperarConductor(int id_conductor)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                conductor con = db.conductor.Find(id_conductor);
                return con;
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Editar(conductor con)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                conductor conAux = db.conductor.Find(con.id_conductor);
                conAux.id_conductor = con.id_conductor;
                conAux.nombre_conductor = con.nombre_conductor;
                conAux.nro_tarjeta_credito = con.nro_tarjeta_credito;
                conAux.telefono_contacto = con.telefono_contacto;
                conAux.DNI = con.DNI;

                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public bool ExisteDNIConductor(int DNI, int id_conductor)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                conductor con = db.conductor.Where(c => (c.DNI == DNI && c.id_conductor!= id_conductor)).FirstOrDefault();
                if (con == null) return true;
                return false;
            }
        }
//-----------------------------------------------------------------------------------------------
        public void Eliminar(int id_conductor)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                conductor con = db.conductor.Find(id_conductor);
                db.conductor.Remove(con);
                db.SaveChanges();
            }
        }
    }
}