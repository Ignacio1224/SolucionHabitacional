using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    interface IRepositorioVivienda
    {
        Boolean Add(Vivienda vi);

        Boolean Delete(Vivienda vi);

        Boolean Update(Vivienda vi);

        Barrio FindByLocation(String street, int num);

        IEnumerable<Vivienda> FindAll();

        Boolean GenerateReports();
    }
}
