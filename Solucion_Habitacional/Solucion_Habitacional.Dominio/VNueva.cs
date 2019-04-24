using Solucion_Habitacional.Dominio.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio
{
    public class VNueva : Vivienda
    {

        public double precio_final { get; set; }

        public override double CalcularPrecio()
        {
            Repositorios.ADO.RepositorioParametro rp = new Repositorios.ADO.RepositorioParametro();
            double cf = precio_base * Math.Pow((1 + Convert.ToDouble(rp.FindByName("interes").valor) / 100), Convert.ToDouble(rp.FindByName("plazo_fijo_vusada").valor));

            cf += cf * (Convert.ToDouble(rp.FindByName("itp").valor) / 100);

            return cf;
        }

        public override Boolean Insertar()
        {
            Boolean flag = base.Insertar();

            if (flag && base.Es_Nueva())
            {
                flag = false;

                String query = @"Insert_VNUEVA";
                SqlConnection cn = UtilidadesDB.CreateConnection();
                UtilidadesDB.OpenConnection(cn);
                SqlTransaction trn = cn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, cn, trn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    precio_final = CalcularPrecio();

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@precio_final", precio_final));

                    flag = (int) cmd.ExecuteScalar() > 0;

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
            }
            
            return flag;
        }

        public override Boolean Modificar()
        {
            Boolean flag = false;

            if (base.Es_Nueva())
            {
                String query = @"Update_Vivienda";
                SqlConnection cn = UtilidadesDB.CreateConnection();
                UtilidadesDB.OpenConnection(cn);
                SqlTransaction trn = cn.BeginTransaction();
                precio_final = CalcularPrecio();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, cn, trn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@calle", calle));
                    cmd.Parameters.Add(new SqlParameter("@nro_puerta", nro_puerta));
                    cmd.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@nro_banios", nro_banios));
                    cmd.Parameters.Add(new SqlParameter("@nro_dormitorios", nro_dormitorios));
                    cmd.Parameters.Add(new SqlParameter("@superficie", superficie));
                    cmd.Parameters.Add(new SqlParameter("@anio_construccion", anio_construccion));
                    cmd.Parameters.Add(new SqlParameter("@precio_base", precio_base));
                    cmd.Parameters.Add(new SqlParameter("@habilitada", convertBooleanToChar(habilitada)));
                    cmd.Parameters.Add(new SqlParameter("@vendida", convertBooleanToChar(vendida)));
                    cmd.Parameters.Add(new SqlParameter("@barrio", barrio.nombre));
                    cmd.Parameters.Add(new SqlParameter("@precio_final", precio_final));

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
            }

            return flag;
        }

        public override string ToString()
        {
            return base.ToString() 
                + "\n\t\t" + "Precio final: " + precio_final;
        }
    }
}
