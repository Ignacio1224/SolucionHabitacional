using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Solucion_Habitacional.Dominio.Repositorios.ADO
{
    public class RepositorioVivienda : InterfacesRepositorio.IRepositorioVivienda
    {

        private RepositorioBarrio repoBarrio = new RepositorioBarrio();

        public Boolean Add(Vivienda v)
        {
            return v != null && v.Validar() && v.Insertar();
        }

        public Boolean Delete(Vivienda vi)
        {
            return vi != null && vi.Eliminar();
        }

        public IEnumerable<Vivienda> FindAll()
        {
            List<Vivienda> listaViviendas = new List<Vivienda>();
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT * 
                                FROM VIVIENDA V INNER JOIN VUSADA VU ON V.id = VU.vivienda;";

                SqlCommand cmd = new SqlCommand(query, cn, trn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaViviendas.Add(new VUsada
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"]),
                            contribucion = Convert.ToDouble(dr["contribucion"])
                        });
                    }
                }

                dr.Close();
                query = @"SELECT * 
                          FROM VIVIENDA V INNER JOIN VNUEVA VN ON V.id = VN.vivienda;";
                cmd = new SqlCommand(query, cn, trn);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaViviendas.Add(new VNueva
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"])
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

            return listaViviendas;
        }

        public IEnumerable<Vivienda> FindByLocation(Barrio b)
        {
            List<Vivienda> listaViviendas = new List<Vivienda>();
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT * 
                                FROM VIVIENDA V INNER JOIN VUSADA VU ON V.id = VU.vivienda
                                WHERE V.barrio = @barrio;";

                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@barrio", b.nombre));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaViviendas.Add(new VUsada
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"]),
                            contribucion = Convert.ToDouble(dr["contribucion"])
                        });
                    }
                }

                dr.Close();
                query = @"SELECT * 
                          FROM VIVIENDA V INNER JOIN VNUEVA VN ON V.id = VN.vivienda
                          WHERE V.barrio = @barrio;";
                cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@barrio", b.nombre));
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listaViviendas.Add(new VNueva
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"])
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

            return listaViviendas;
        }

        public Vivienda FindById(int id)
        {
            Vivienda v = null;
            SqlConnection cn = UtilidadesDB.CreateConnection();
            UtilidadesDB.OpenConnection(cn);
            SqlTransaction trn = cn.BeginTransaction();

            try
            {
                String query = @"SELECT * 
                                FROM VIVIENDA V INNER JOIN VUSADA VU ON V.id = VU.vivienda
                                WHERE V.id = @id;";

                SqlCommand cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@id", id));

                /* [-1 --> No Existe; 0 --> Nueva; 1 --> Usada] */
                

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        v = new VUsada
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"]),
                            contribucion = Convert.ToDouble(dr["contribucion"])
                        };
                    }
                }

                dr.Close();
                query = @"SELECT * 
                          FROM VIVIENDA V INNER JOIN VNUEVA VN ON V.id = VN.vivienda
                          WHERE V.id = @id;";
                cmd = new SqlCommand(query, cn, trn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        v = new VNueva
                        {
                            calle = dr["calle"].ToString(),
                            nro_puerta = Convert.ToInt32(dr["nro_puerta"]),
                            barrio = repoBarrio.FindByName(dr["barrio"].ToString()),
                            descripcion = dr["descripcion"].ToString(),
                            nro_banios = Convert.ToInt32(dr["nro_banios"]),
                            nro_dormitorios = Convert.ToInt32(dr["nro_dormitorios"]),
                            superficie = Convert.ToDouble(dr["superficie"]),
                            precio_base = Convert.ToDouble(dr["precio_base"]),
                            anio_construccion = Convert.ToInt32(dr["anio_construccion"]),
                            vendida = ConvertCharToBoolean(Convert.ToChar(dr["vendida"].ToString())),
                            habilitada = ConvertCharToBoolean(Convert.ToChar(dr["habilitada"].ToString())),
                            id = Convert.ToInt32(dr["id"]),
                            precio_final = Convert.ToDouble(dr["precio_base"]),
                            tipo = Convert.ToInt32(dr["tipo"])
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

            return v;
        }

        public Boolean GenerateReports()
        {
            Boolean flag = false;

            try
            {
                var lista = FindAll();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\..\Archivos\Viviendas.txt"))
                {
                    foreach (Vivienda v in lista)
                    {
                        file.WriteLine(v.DatosReporte());
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

        public Boolean Update(Vivienda vi)
        {
            return vi != null && vi.Modificar();
        }

        private Boolean ConvertCharToBoolean(char d)
        {
            return d == '1' ? true : false;
        }
    }
}
