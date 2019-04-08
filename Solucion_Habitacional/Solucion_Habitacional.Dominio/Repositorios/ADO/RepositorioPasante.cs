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
            throw new NotImplementedException();
        }

        public Barrio FindByName(string username)
        {
            throw new NotImplementedException();
        }

        public bool GenerateReports()
        {
            throw new NotImplementedException();
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
