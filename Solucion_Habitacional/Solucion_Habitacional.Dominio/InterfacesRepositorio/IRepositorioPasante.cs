using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    public interface IRepositorioPasante
    {
        Boolean Add(Pasante p);

        Boolean Delete(Pasante p);

        Boolean Update(Pasante p);

        Pasante FindByName(string nombre);

        IEnumerable<Pasante> FindAll();

        Boolean Ingresar(Pasante p);
    }
}
