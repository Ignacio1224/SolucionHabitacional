using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Solucion_Habitacional.Dominio.Utilidades;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Solucion_Habitacional.Dominio
{
    public class Parametro : IEquatable<Parametro>, IActiveRecord
    {
        public String nombre { get; set; }
        public String valor { get; set; }

        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"Delete_Parametro";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(stringCommand, cn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                flag = (int)cmd.ExecuteScalar() == 1;
                trn.Commit();
            }
            catch (SqlException ex)
            {
                trn.Rollback();
                Debug.WriteLine("Error al Eliminar " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Otro Error " + ex.Message);
            }
            finally
            {
                UtilidadesDB.CloseConnection(cn);
                cn.Dispose();
            }
            return flag;
        }

        public Boolean Equals(Parametro other)
        {
            return nombre == other.nombre;
        }

        public Boolean Insertar()
        {
            Boolean flag = false;
            String query = @"Insert_Parametro";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@valor", valor));
                flag = (int)cmd.ExecuteScalar() == 1;
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

        public Boolean Modificar()
        {
            Boolean flag = false;
            String query = @"Update_Parametro";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@valor", valor));

                flag = (int)cmd.ExecuteScalar() == 1;
                trn.Commit();
            }
            catch (SqlException e)
            {
                trn.Rollback();
                Console.WriteLine("Se ha producido un error " + e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
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
            return nombre + "=" + valor;
        }

        public Boolean Validar()
        {
            return nombre != null && nombre.Length > 0 && valor != null && valor.Length > 0;
        }
    }
}
