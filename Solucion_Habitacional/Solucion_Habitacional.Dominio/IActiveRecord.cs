using System;

namespace Solucion_Habitacional.Dominio
{
    interface IActiveRecord
    {
        Boolean Insertar();

        Boolean Eliminar();

        Boolean Modificar();

    }
}
