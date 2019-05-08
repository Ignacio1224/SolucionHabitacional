using Solucion_Habitacional.Dominio;
using Solucion_Habitacional.Dominio.Repositorios.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Solucion_Habitacional.Servicio
{
    public class ServicioVivienda : IServicioVivienda
    {
        RepositorioVivienda repoV = new RepositorioVivienda();
        RepositorioBarrio repoB = new RepositorioBarrio();

        public bool Agregar(string calle, int nro_puerta, DtoBarrio barrio, string descripcion, int nro_banios, int nro_dormitorios, double superficie, 
            double precio_base, int anio_construccion, bool habilitada, bool vendida, bool nueva)
        {
            if (nueva)
            {
                return repoV.Add(new VNueva {
                    calle = calle,
                    nro_puerta = nro_puerta,
                    barrio = repoB.FindByName(barrio.name),
                    descripcion = descripcion,
                    nro_banios = nro_banios,
                    nro_dormitorios = nro_dormitorios,
                    superficie = superficie,
                    precio_base = precio_base,
                    anio_construccion = anio_construccion,
                    vendida = vendida,
                    habilitada = habilitada,
                    tipo = 0
                });
            } else
            {
                return repoV.Add(new VUsada
                {
                    calle = calle,
                    nro_puerta = nro_puerta,
                    barrio = repoB.FindByName(barrio.name),
                    descripcion = descripcion,
                    nro_banios = nro_banios,
                    nro_dormitorios = nro_dormitorios,
                    superficie = superficie,
                    precio_base = precio_base,
                    anio_construccion = anio_construccion,
                    vendida = vendida,
                    habilitada = habilitada,
                    tipo = 1
                });
            }
        }

        public IEnumerable<DtoVivienda> GetViviendas(DtoBarrio b)
        {
            IEnumerable<DtoVivienda> viviendas = new List<DtoVivienda>();
            List<DtoVivienda> aux_viviendas = new List<DtoVivienda>();
            
            aux_viviendas.AddRange(ObjectConversor.ConvertToDtoVivienda(repoV.FindByLocation(ObjectConversor.ConvertToBarrio(b))));

            viviendas = aux_viviendas;
            return viviendas;
        }

        public IEnumerable<DtoVivienda> FindAll()
        {
            IEnumerable<DtoVivienda> viviendas = new List<DtoVivienda>();

            viviendas = ObjectConversor.ConvertToDtoVivienda(repoV.FindAll());

            return viviendas;
        }

        public Boolean GenerateReport()
        {
            return repoV.GenerateReports();
        }

        public Boolean Modificar(DtoVivienda v)
        {
            return v != null && repoV.Update(ObjectConversor.ConvertToVivienda(v));
        }

        public Boolean Eliminar(DtoVivienda v)
        {
            return repoV.Delete(ObjectConversor.ConvertToVivienda(v));
        }

        public DtoVivienda FindById(int id)
        {
            return ObjectConversor.ConvertToDtoVivienda(repoV.FindById(id));
        }
    }
}
