using System;
using System.Data.SqlClient;
using System.Data;
using Solucion_Habitacional.Dominio.Utilidades;

namespace Solucion_Habitacional.Dominio
{
    public class Barrio : IEquatable<Barrio>, IActiveRecord 
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public Boolean Validar ()
        {
            return nombre != null && descripcion != null && nombre.Length > 0 && descripcion.Length > 0;
        }

        public Boolean Equals (Barrio other)
        {
            return other != null && this.nombre == other.nombre;
        }

        public override string ToString ()
        {
            return "Nombre: " + nombre + " - Descripción: " + descripcion;
        }

        public Boolean Insertar ()
        {
            Boolean flag = false;
            String query = @"Insert_Barrio";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
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

        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"Delete_Barrio";

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

        public Boolean Modificar()
        {
            Boolean flag = false;
            String query = @"Update_Barrio";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));

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

    }
}
