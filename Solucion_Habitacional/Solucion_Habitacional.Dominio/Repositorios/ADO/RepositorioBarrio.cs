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

        public Boolean Add (Barrio ba)
        {
            return ba != null && ba.Validar() && !listaBarrios.Contains(ba) && ba.Insertar();
        }

        public Boolean Delete (Barrio ba)
        {
            return ba.Eliminar();
        }

        public Boolean Update (Barrio ba)
        {
            return ba != null && ba.Modificar();
        }

        public Barrio FindByName(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Barrio> FindAll()
        {
            List<Barrio> listaBarrios = new List<Barrio>();

            try
            {
                String query = @"SELECT * FROM BARRIO";
                SqlConnection cn = UtilidadesDB.CreateConnection();
                SqlCommand cmd = new SqlCommand(query, cn);
                UtilidadesDB.OpenConnection(cn);
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

                UtilidadesDB.CloseConnection(cn);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



            return listaBarrios;
        }

        public Boolean GenerateReports()
        {
            throw new NotImplementedException();
        }
    }
}
