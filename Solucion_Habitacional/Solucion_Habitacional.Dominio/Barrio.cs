using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Solucion_Habitacional.Dominio.Utilidades;
using System.Diagnostics;

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
            // Prepare Command
            string query = @"INSERT INTO BARRIO (nombre, descripcion) VALUES (@nombre, @descripcion)";
            try
            {
                SqlConnection cn = UtilidadesDB.CreateConnection();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                UtilidadesDB.OpenConnection(cn);
                cmd.ExecuteNonQuery();
                UtilidadesDB.CloseConnection(cn);
                flag = true;
            }
            catch (SqlException e)
            {
                if (e.Number == 2627) // Violation PK
                {
                    Console.WriteLine("El barrio con el nombre: " + nombre + " ya existe");
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

            return flag;
        }

        public Boolean Eliminar()
        {
            Boolean flag = false;
            String stringCommand = @"DELETE FROM BARRIO WHERE nombre = @nombre; 
                                    SELECT CAST(COUNT (nombre) AS INT) AS 'deleted' FROM BARRIO WHERE nombre = @nombre;";

            SqlConnection cn = UtilidadesDB.CreateConnection();
            SqlCommand cmd = new SqlCommand(stringCommand, cn);
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));

            try
            {
                UtilidadesDB.OpenConnection(cn);
                int deleted = (int)cmd.ExecuteScalar();
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

        public Boolean Modificar()
        {
            Boolean flag = false;
            // Prepare Command
            String query = @"UPDATE BARRIO SET descripcion = @desc WHERE nombre = @name";
            SqlConnection cn = UtilidadesDB.CreateConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add(new SqlParameter("@name", nombre));
                cmd.Parameters.Add(new SqlParameter("@desc", descripcion));

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
    }
}
