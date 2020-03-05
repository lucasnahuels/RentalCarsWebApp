using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrabajoCGB.Models
{
    public class MantenimientoOficina
    {
        public oficina RecuperarOficina(int codigo_unico_oficina)
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                oficina ofi = db.oficina.Find(codigo_unico_oficina);
                return ofi;
            }
        }
//-----------------------------------------------------------------------------------------------
        public List<oficina> RecuperarOficinas()
        {
            using (var db = new BDDAlquilerAutosEntities())
            {
                List<oficina> oficinas = new List<oficina>();
                oficinas = db.oficina.ToList();
                return oficinas;
            }
        }
//-----------------------------------------------------------------------------------------------
        public List<OficinaAux> CrearOficinaAux()
        {
            using (var db = new BDDAlquilerAutosEntities())
            { 
                var data = from o in db.oficina
                           join c in db.ciudad on o.id_ciudad equals c.id_ciudad
                           select new OficinaAux()
                           {
                               codigo_unico_oficina = o.codigo_unico_oficina,
                               calle = o.calle,
                               numero = o.numero,
                               telefono = o.telefono,
                               id_ciudad = o.id_ciudad,
                               nombre_ciudad = c.nombre_ciudad
                           }; 
                return data.ToList();
            }

            /*Otra forma de hacerlo
            string cityName = "El Palomar";
            string sql = @" select o.codigo_unico_oficina, o.calle, o.numero, o.telefono, o.id_ciudad, c.nombre_ciudad
                            from oficina o
                            inner join ciudad c on o.id_ciudad=c.id_ciudad
                            where c.nombre_ciudad = @cityName ";

            return db.database.SqlQuery<OficinaAux>(sql, new SqlParameter("@cityName", cityName), new SqlParameter("@segundoParametro", segundoParametro)).toList() ) //new SqlParametrer sirve para evitar las inyecciones de código automaticamente. Por ejemplo escapa las comillas
            */
        }

//-----------------------------------------------------------------------------------------------
        public void Agregar(oficina ofi)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())//using abre la conexion y la cierra, lo que no ocurre si hago un new database(en cuyo caso la conexion queda abierta, a menos que hagamos un dispose)
            {
                db.oficina.Add(ofi);
                db.SaveChanges();
            }
        }

//-----------------------------------------------------------------------------------------------
/*------------Variante de Agregar con ADO.NET-------------------ver Web.Config
        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["administracion"].ToString();
            con = new SqlConnection(constr);
        }

        public int Agregar(Oficina ofi)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into oficina(codigo_unico_oficina, calle, numero, telefono, id_ciudad) values (null, @calle,@numero, @telefono, @id_ciudad)", con);
            comando.Parameters.Add("@codigo_unico_oficina", SqlDbType.Int);
            comando.Parameters.Add("@calle", SqlDbType.VarChar);
            comando.Parameters.Add("@numero", SqlDbType.Int);
            comando.Parameters.Add("@telefono", SqlDbType.VarChar);
            comando.Parameters.Add("@id_ciudad", SqlDbType.Int);
            comando.Parameters["@codigo_unico_oficina"].Value = ofi.codigo_unico_oficina;
            comando.Parameters["@calle"].Value = ofi.calle;
            comando.Parameters["@numero"].Value = ofi.numero;
            comando.Parameters["@telefono"].Value = ofi.telefono;
            comando.Parameters["@id_ciudad"].Value = ofi.id_ciudad;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }*/

//-----------------------------------------------------------------------------------------------
        public void Modificar(oficina ofi)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                oficina ofiAux = db.oficina.Find(ofi.codigo_unico_oficina);
                ofiAux.codigo_unico_oficina = ofi.codigo_unico_oficina;
                ofiAux.calle = ofi.calle;
                ofiAux.numero = ofi.numero;
                ofiAux.telefono = ofi.telefono;
                ofiAux.id_ciudad = ofi.id_ciudad;

                db.SaveChanges();
            }
        }

//-----------------------------------------------------------------------------------------------
/*Variante de Modificar() con ADO.NET
        public int Modificar(Oficina ofi)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update oficina set codigo_unico_oficina=@codigo_unico_oficina, calle=@calle, numero=@numero, telefono=@telefono, id_ciudad=@id_ciudad where codigo_unico_oficina=@codigo_unico_oficina", con);
            comando.Parameters.Add("@codigo_unico_oficina", SqlDbType.Int);
            comando.Parameters["@codigo_unico_oficina"].Value = ofi.codigo_unico_oficina;
            comando.Parameters.Add("@calle", SqlDbType.VarChar);
            comando.Parameters["@calle"].Value = ofi.calle;
            comando.Parameters.Add("@numero", SqlDbType.Int);
            comando.Parameters["@numero"].Value = ofi.numero;
            comando.Parameters.Add("@telefono", SqlDbType.VarChar);
            comando.Parameters["@telefono"].Value = ofi.telefono;            
            comando.Parameters.Add("@id_ciudad", SqlDbType.Int);
            comando.Parameters["@id_ciudad"].Value = ofi.id_ciudad;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i; //i devuelve cantidad de filas afectadas
        }*/

//-----------------------------------------------------------------------------------------------
        public void Eliminar(int cod)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                oficina ofi= db.oficina.Find(cod);
                db.oficina.Remove(ofi);
                db.SaveChanges();
            }
        }
//-----------------------------------------------------------------------------------------------
        public string RecuperarDireccionOficina(int codigo_unico_oficina)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                string calle= db.oficina.Find(codigo_unico_oficina).calle;
                string numero= db.oficina.Find(codigo_unico_oficina).numero.ToString();
                string direccion = calle + numero;
                return direccion;
            }
        }

 //-----------------------------------------------------------------------------------------------       
        public bool ExisteDireccionOficina(int id_ciudad, string calle, int numero, int cod)
        {
            using (BDDAlquilerAutosEntities db = new BDDAlquilerAutosEntities())
            {
                oficina ofi = db.oficina.Where(o => (o.id_ciudad == id_ciudad && o.calle == calle && o.numero==numero && o.codigo_unico_oficina != cod) ).FirstOrDefault();
                if (ofi == null) return true;
                return false;
            }
        }

    }
}