using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Solucion_Habitacional.Dominio.Repositorios.ADO
{
    public class RepositorioParametro : InterfacesRepositorio.IRepositorioParametro
    {
        private List<Parametro> listaParametros = new List<Parametro>();


        public Boolean Add(Parametro p)
        {
            return p != null && p.Validar() && !listaParametros.Contains(p) && p.Insertar();
        }

        public Boolean Delete(Parametro p)
        {
            return p.Eliminar();
        }

        public IEnumerable<Parametro> FindAll()
        {
            List<Parametro> listaParametros = new List<Parametro>();
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT nombre, valor FROM PARAMETRO";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaParametros.Add(new Parametro
                        {
                            nombre = dr["nombre"].ToString(),
                            valor = dr["valor"].ToString()
                        });
                    }
                }

                UtilidadesDB.CloseConnection(cn);

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

            return listaParametros;
        }

        public Parametro FindByName(string nombre)
        {
            Parametro p = null;
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT nombre, valor FROM PARAMETRO WHERE nombre = @nombre";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        p = new Parametro
                        {
                            nombre = dr["nombre"].ToString(),
                            valor = dr["valor"].ToString()
                        };
                    }
                }
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

            return p;
        }

        public Boolean GenerateReports()
        {
            Boolean flag = false;

            try
            {
                var lista = FindAll();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\Parametros.txt"))
                {
                    foreach (Parametro p in lista)
                    {
                        file.WriteLine(p.ToString() + "#");
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

        public Boolean Update(Parametro p)
        {
            return p.Validar() && p.Modificar();
        }
    }
}
