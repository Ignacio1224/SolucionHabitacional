using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    interface IRepositorioBarrio
    {
        Boolean Add(Barrio ba);

        Boolean Delete(Barrio ba);

        Boolean Update(Barrio ba);

        Barrio FindByName(string nombre);

        IEnumerable<Barrio> FindAll();

        Boolean GenerateReports();

    }
}
