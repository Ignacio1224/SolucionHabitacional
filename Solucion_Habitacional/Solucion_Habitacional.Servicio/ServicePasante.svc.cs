using System;
using System.Collections.Generic;
using Solucion_Habitacional.Dominio;
using Solucion_Habitacional.Dominio.Repositorios.ADO;

namespace Solucion_Habitacional.Servicio
{
    public class ServicePasante : IServicePasante
    {
        private RepositorioPasante repoP = new RepositorioPasante();

        public Boolean Agregar(String user_name, String password)
        {
            return repoP.Add(new Pasante
            {
                user_name = user_name,
                password = password
            });
        }

        public Boolean Eliminar(string user_name)
        {
            return repoP.Delete(ObjectConversor.ConvertToPasante(GetPasante(user_name)));
        }

        public DtoPasante GetPasante(String user_name)
        {
            return ObjectConversor.ConvertToDtoPasante(repoP.FindByName(user_name));
        }

        public Boolean Ingresar(DtoPasante p)
        {
            return repoP.Ingresar(ObjectConversor.ConvertToPasante(p));
        }

        public Boolean Modificar(string user_name, string password)
        {
            return repoP.Update(new Pasante
            {
                user_name = user_name,
                password = password
            });
        }

        public IEnumerable<DtoPasante> ObtenerTodos()
        {
            IEnumerable<DtoPasante> lista_dto_pasantes = new List<DtoPasante>();

            lista_dto_pasantes = ObjectConversor.ConvertToDtoPasante(repoP.FindAll());

            return lista_dto_pasantes;
        }
    }
}
