using System;
using System.Collections.Generic;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    public interface IRepositorioVivienda
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
