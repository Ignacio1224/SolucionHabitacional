using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    interface IRepositorioVivienda
    {
        Boolean Add(Vivienda v);

        Boolean Delete(Vivienda vi);

        Boolean Update(Vivienda vi);

        IEnumerable<Vivienda> FindByLocation(Barrio b);

        Vivienda FindById(int id);

        IEnumerable<Vivienda> FindAll();

        Boolean GenerateReports();
    }
}
