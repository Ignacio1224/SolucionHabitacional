using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solucion_Habitacional.Dominio.Utilidades;
using System.Data.SqlClient;

namespace Solucion_Habitacional.Dominio.Repositorios.ADO
{
    public class RepositorioPasante : InterfacesRepositorio.IRepositorioPasante
    {
        private List<Pasante> listaPasante = new List<Pasante>();

        public bool Add(Pasante p)
        {
            return p != null && p.Validar() && !listaPasante.Contains(p) && p.Insertar();
        }

        public bool Delete(Pasante p)
        {
            return p != null && p.Eliminar();
        }

        public IEnumerable<Pasante> FindAll()
        {
            List<Pasante> listaPasantes = new List<Pasante>();
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT userName FROM PASANTE";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaPasantes.Add(new Pasante
                        {
                            user_name = dr["userName"].ToString(),
                            password = ""
                        });
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

            return listaPasantes;
        }

        public Pasante FindByName(string username)
        {
            Pasante p = null;
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT userName FROM PASANTE WHERE useName = @username";
                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                p = new Pasante {
                    user_name = (String)cmd.ExecuteScalar(),
                    password = null
                };
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

        public bool Update(Pasante p)
        {
            return p != null && p.Validar() && p.Modificar(); // Saco el contains p in listaPasante
        }

        public Boolean Ingresar(Pasante p)
        {
            return p.Validar() && p.Ingresar();
        }

    }
}
