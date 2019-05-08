using System;
using System.Collections.Generic;
using Solucion_Habitacional.Dominio;
using Solucion_Habitacional.Dominio.Repositorios.ADO;

namespace Solucion_Habitacional.Servicio
{
    public class ServicioBarrio : IServicioBarrio
    {
        private RepositorioBarrio repoB = new RepositorioBarrio();

        public Boolean Agregar(String name, String description)
        {
            return repoB.Add(new Barrio
            {
                nombre = name,
                descripcion = description
            });
        }

        public bool Eliminar(string name)
        {
            return repoB.Delete(ObjectConversor.ConvertToBarrio(GetBarrio(name)));
        }

        public bool GenerateReport()
        {
            return repoB.GenerateReports();
        }

        public DtoBarrio GetBarrio(String name)
        {
            return ObjectConversor.ConvertToDtoBarrio(repoB.FindByName(name));
        }

        public Boolean Modificar(string name, string description)
        {
            return repoB.Update(new Barrio
            {
                nombre = name,
                descripcion = description
            });
        }

        public IEnumerable<DtoBarrio> ObtenerTodos()
        {
            IEnumerable<DtoBarrio> lista_dto_barrios = new List<DtoBarrio>();

            lista_dto_barrios = ObjectConversor.ConvertToDtoBarrio(repoB.FindAll());

            return lista_dto_barrios;
        }
    }
}
