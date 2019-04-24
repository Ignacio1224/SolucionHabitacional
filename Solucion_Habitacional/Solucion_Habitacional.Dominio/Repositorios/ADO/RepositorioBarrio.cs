using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solucion_Habitacional.Dominio;
using System.Data;
using Solucion_Habitacional.Dominio.Utilidades;
using System.Data.SqlClient;

namespace Solucion_Habitacional.Dominio.Repositorios.ADO
{
    public class RepositorioBarrio : InterfacesRepositorio.IRepositorioBarrio
    {
        private List<Barrio> listaBarrios = new List<Barrio>();

        public Boolean Add(Barrio ba)
        {
            return ba != null && ba.Validar() && !listaBarrios.Contains(ba) && ba.Insertar();
        }

        public Boolean Delete(Barrio ba)
        {
            return ba.Eliminar();
        }

        public Boolean Update(Barrio ba)
        {
            return ba != null && ba.Modificar();
        }

        public Barrio FindByName(string nombre)
        {
            Barrio b = null;
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT nombre, descripcion FROM BARRIO WHERE nombre = @nombre";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        b = new Barrio
                        {
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString()
                        };
                    }
                }
                dr.Close();
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

            return b;
        }

        public IEnumerable<Barrio> FindAll()
        {
            List<Barrio> listaBarrios = new List<Barrio>();
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT nombre, descripcion FROM BARRIO";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaBarrios.Add(new Barrio
                        {
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString()
                        });
                    }
                }
                dr.Close();
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

            return listaBarrios;
        }

        public Boolean GenerateReports()
        {
            Boolean flag = false;

            try
            {
                var lista = FindAll();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\..\Archivos\Barrios.txt"))
                {
                    foreach (Barrio b in lista)
                    {
                        file.WriteLine(b.nombre + "#" + b.descripcion);
                    }
                }

                flag = true;
            }
            catch (Exception e)
            {
                flag = false;
                Console.WriteLine("Error " + e.Message);
            }
            return flag;
        }
    }
}
