using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Solucion_Habitacional.Dominio.Utilidades;

namespace Solucion_Habitacional.Dominio
{
    public class Barrio : IEquatable<Barrio>, IActiveRecord 
    {
        private string nombre { get; set; }
        private string descripcion { get; set; }

        public Boolean Validar ()
        {
            return this.nombre != null && this.descripcion != null && this.nombre.Length > 0 && this.descripcion.Length > 0;
        }

        public Boolean Equals (Barrio other)
        {
            return other != null && this.nombre == other.nombre;
        }

        public override string ToString ()
        {
            return "Nombre: " + this.nombre + " - Descripción: " + this.descripcion;
        }

        public Boolean Insertar ()
        {
            throw new NotImplementedException();
        }

        public Boolean Eliminar()
        {
            throw new NotImplementedException();
        }

        public Boolean Modificar()
        {
            throw new NotImplementedException();
        }
    }
}
