using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    public interface IRepositorioParametro
    {
        Boolean Add(Parametro p);

        Boolean Delete(Parametro p);

        Boolean Update(Parametro p);

        Parametro FindByName(string nombre);

        IEnumerable<Parametro> FindAll();

        Boolean GenerateReports();
    }
}
