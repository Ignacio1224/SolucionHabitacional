using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio
{
    public abstract class Vivienda : IEquatable<Vivienda>, IActiveRecord
    {

        #region parameters
        public int id { get; set; }
        public String calle { get; set; }
        public int nro_puerta { get; set; }
        public Barrio barrio { get; set; }
        public String descripcion { get; set; }
        public int nro_banios { get; set; }
        public int nro_dormitorios { get; set; }
        public double superficie { get; set; }
        public double precio_base { get; set; }
        public int anio_construccion { get; set; }
        public Boolean vendida { get; set; } = false;
        public Boolean habilitada { get; set; } = false;
        #endregion

        public virtual Boolean Validar()
        {
            return
                calle               != null &&
                calle               != "" &&
                nro_puerta          > 0 &&
                barrio              != null && 
                descripcion         != null &&
                nro_banios          >= 0 &&
                nro_dormitorios     > 0 &&
                superficie          > 0 &&
                precio_base         > 0 &&
                anio_construccion   > -10000;
        }

        public virtual Boolean Es_Nueva()
        {
            Repositorios.ADO.RepositorioParametro repoParam = new Repositorios.ADO.RepositorioParametro();
            Parametro p = repoParam.FindByName("tope_metraje_vnueva");
            int current_year = DateTime.Now.Year;
            int res = Convert.ToInt16(repoParam.FindByName("anio_nueva").valor) | 2;

            return current_year - anio_construccion <= res && Convert.ToDouble(p.valor) > superficie;
        }

        public abstract double CalcularPrecio();

        public virtual Boolean Insertar()
        {
            Boolean flag = false;

            String query = @"Insert_Vivienda";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@calle", calle));
                cmd.Parameters.Add(new SqlParameter("@nro_puerta", nro_puerta));
                cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                cmd.Parameters.Add(new SqlParameter("@nro_banios", nro_banios));
                cmd.Parameters.Add(new SqlParameter("@nro_dormitorios", nro_dormitorios));
                cmd.Parameters.Add(new SqlParameter("@superficie", superficie));
                cmd.Parameters.Add(new SqlParameter("@anio_construccion", anio_construccion));
                cmd.Parameters.Add(new SqlParameter("@precio_base", precio_base));
                cmd.Parameters.Add(new SqlParameter("@habilitada", convertBooleanToChar(habilitada)));
                cmd.Parameters.Add(new SqlParameter("@vendida", convertBooleanToChar(vendida)));
                cmd.Parameters.Add(new SqlParameter("@barrio", barrio.nombre));

                id = (int)cmd.ExecuteScalar();

                flag = id > 0;

                trn.Commit();
                    
            }
            catch (SqlException e)
            {
                trn.Rollback();
                Console.WriteLine("Se ha producido un error " + e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                UtilidadesDB.CloseConnection(cn);
                cn.Dispose();
            }
            
            return flag;
        }

        public abstract Boolean Modificar();

        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"Delete_Vivienda";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(stringCommand, cn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                flag = (int) cmd.ExecuteScalar() == 1;
                trn.Commit();
            }
            catch (SqlException ex)
            {
                trn.Rollback();
                Console.WriteLine("Error al Eliminar " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Otro Error " + ex.Message);
            }
            finally
            {
                UtilidadesDB.CloseConnection(cn);
                cn.Dispose();
            }
            return flag;
        }

        public override string ToString()
        {
            return "\n\t\t" + "Id: " + id + "\n\t\t" + "Dirección: " + calle + " " + nro_puerta + "\n\t\t" + "Barrio: " + barrio.nombre + "\n\t\t" 
                + "Descripción: " + descripcion + "\n\t\t" + "Cantidad de baños: " + nro_banios + "\n\t\t" 
                + "Cantidad de dormitorios: " + nro_dormitorios + "\n\t\t" + "Superficie: " + superficie + "\n\t\t" + "Precio base: " + precio_base + "\n\t\t"
                + "Año de construcción: " + anio_construccion + "\n\t\t" + "Vendida: " + (vendida ? "Si" : "No") + "\n\t\t" 
                + "Habilitada: " + (habilitada ? "Si" : "No");
        }

        protected char convertBooleanToChar(Boolean d)
        {
            return d ? '1' : '0';
        }

        public Boolean Equals(Vivienda other)
        {
            return id == other.id;
        }
    }
}
