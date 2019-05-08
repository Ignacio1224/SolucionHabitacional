using System;
using System.Collections.Generic;

namespace Solucion_Habitacional.Dominio.InterfacesRepositorio
{
    public interface IRepositorioBarrio
    {
        Boolean Add(Barrio ba);

        Boolean Delete(Barrio ba);

        Boolean Update(Barrio ba);

        Barrio FindByName(string nombre);

        IEnumerable<Barrio> FindAll();

        Boolean GenerateReports();

    }
}
