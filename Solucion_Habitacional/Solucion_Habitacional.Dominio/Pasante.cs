using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio
{
    public class Pasante : IEquatable<Pasante>, IActiveRecord
    {

        public String user_name { get; set; }
        public String password { get; set; }


        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"DELETE FROM PASANTE WHERE userName = @username AND PWDCOMPARE(@password, userPassword) = 1; 
                                    SELECT CAST(COUNT (userName) AS INT) AS 'deleted' FROM PASANTE WHERE userName = @username;";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            SqlCommand cmd = new SqlCommand(stringCommand, cn);
            cmd.Parameters.Add(new SqlParameter("@username", user_name));
            cmd.Parameters.Add(new SqlParameter("@password", password));


            try
            {
                UtilidadesDB.OpenConnection(cn);
                int deleted = (int) cmd.ExecuteScalar();
                flag = deleted == 0;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message + "Error al Eliminar");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "Otro Error");
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
            throw new NotImplementedException();
        }

        public Boolean Ingresar()
        {
            Boolean flag = false;
            String stringCommand = @"SELECT userName, userPassword FROM PASANTE WHERE userName = @username AND PWDCOMPARE(@password, userPassword) = 1;";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            SqlCommand cmd = new SqlCommand(stringCommand, cn);
            cmd.Parameters.Add(new SqlParameter("@username", user_name));
            cmd.Parameters.Add(new SqlParameter("@password", password));


            try
            {
                UtilidadesDB.OpenConnection(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Pasante p = new Pasante
                        {
                            user_name = dr["userName"].ToString(),
                            password = dr["userPassword"].ToString()
                        };

                        flag = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message + "Error al Ingresar");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "Otro Error");
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
            // Prepare Command
            String query = @"INSERT INTO PASANTE (userName, userPassword) VALUES (@user_name, PWDENCRYPT(@password))";
            SqlConnection cn = UtilidadesDB.CreateConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                UtilidadesDB.OpenConnection(cn);
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException e)
            {
                if (e.Number == 2627) // Violation PK
                {
                    Console.WriteLine("El pasante con el nombre de usuario: " + user_name + " ya existe");
                }
                else
                {
                    Console.WriteLine("Se ha producido un error " + e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Error");
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
            // Prepare Command
            String query = @"UPDATE PASANTE SET userPassword = PWDENCRYPT(@password) WHERE userName = @user_name";
            SqlConnection cn = UtilidadesDB.CreateConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add(new SqlParameter("@password", password));
                cmd.Parameters.Add(new SqlParameter("@user_name", user_name));

                UtilidadesDB.OpenConnection(cn);
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Se ha producido un error " + e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Error");
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
    }
}
