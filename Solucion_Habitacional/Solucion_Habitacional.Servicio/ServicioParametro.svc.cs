using Solucion_Habitacional.Dominio;
using Solucion_Habitacional.Dominio.Repositorios.ADO;
using System;
using System.Collections.Generic;

namespace Solucion_Habitacional.Servicio
{
    public class ServicioParametro : IServicioParametro
    {
        private RepositorioParametro repoP = new RepositorioParametro();

        public Boolean Agregar(String name, String value)
        {
            return repoP.Add(new Parametro
            {
                nombre = name,
                valor = value
            });
        }

        public Boolean Eliminar(String name)
        {
            return repoP.Delete(ObjectConversor.ConvertToParametro(GetParametro(name)));
        }

        public Boolean GenerateReport()
        {
            return repoP.GenerateReports();
        }

        public DtoParametro GetParametro(String name)
        {
            return ObjectConversor.ConvertToDtoParametro(repoP.FindByName(name));
        }

        public Boolean Modificar(string name, string value)
        {
            return repoP.Update(new Parametro
            {
                nombre = name,
                valor = value
            });
        }

        public IEnumerable<DtoParametro> ObtenerTodos()
        {
            IEnumerable<DtoParametro> lista_dto_pasantes = new List<DtoParametro>();

            lista_dto_pasantes = ObjectConversor.ConvertToDtoParametro(repoP.FindAll());

            return lista_dto_pasantes;
        }
    }
}
