using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Solucion_Habitacional.Dominio
{
    public class Pasante : IEquatable<Pasante>, IActiveRecord
    {

        public String user_name { get; set; }
        public String password { get; set; }


        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"Delete_Pasante";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(stringCommand, cn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userName", user_name));
                cmd.Parameters.Add(new SqlParameter("@userPassword", password));
                flag = (int) cmd.ExecuteScalar() == 1;
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

        public Boolean Equals(Pasante other)
        {
            return user_name == other.user_name;
        }

        public Boolean Ingresar()
        {
            Boolean flag = false;
            String query = @"Ingreso_Pasante";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userName", user_name));
                cmd.Parameters.Add(new SqlParameter("@userPassword", password));
                flag = (int)cmd.ExecuteScalar() == 1;   
            }
            catch (SqlException e)
            {
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

        public Boolean Insertar()
        {
            Boolean flag = false;
            String query = @"Insert_Pasante";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userName", user_name));
                cmd.Parameters.Add(new SqlParameter("@userPassword", password));
                flag = (int) cmd.ExecuteScalar() == 1;
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
            String query = @"Update_Pasante";
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userPassword", password));
                cmd.Parameters.Add(new SqlParameter("@userName", user_name));

                flag = (int) cmd.ExecuteScalar() == 1;
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

        public Boolean Validar()
        {
            return user_name != null && user_name != "" && user_name.Contains("@") && password != null && password.Length > 7;
        }

        public override string ToString()
        {
            return user_name;
        }
    }
}
