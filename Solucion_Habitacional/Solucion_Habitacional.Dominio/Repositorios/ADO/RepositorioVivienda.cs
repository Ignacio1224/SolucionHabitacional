using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.Repositorios.ADO
{
    public class RepositorioVivienda : InterfacesRepositorio.IRepositorioVivienda
    {
        public Boolean Add(Vivienda vi)
        {
            throw new NotImplementedException();
        }

        public Boolean Delete(Vivienda vi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vivienda> FindAll()
        {
            throw new NotImplementedException();
        }

        public Barrio FindByLocation(string street, int num)
        {
            throw new NotImplementedException();
        }

        public Boolean GenerateReports()
        {
            throw new NotImplementedException();
        }

        public Boolean Update(Vivienda vi)
        {
            throw new NotImplementedException();
        }
    }
}
