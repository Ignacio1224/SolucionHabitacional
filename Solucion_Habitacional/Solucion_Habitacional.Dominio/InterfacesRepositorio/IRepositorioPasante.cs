using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    interface IRepositorioPasante
    {
        Boolean Add(Pasante p);

        Boolean Delete(Pasante p);

        Boolean Update(Pasante p);

        Barrio FindByName(string nombre);

        IEnumerable<Pasante> FindAll();

        Boolean GenerateReports();

        Boolean Ingresar(Pasante p);
    }
}
