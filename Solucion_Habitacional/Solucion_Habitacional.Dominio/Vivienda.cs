using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio
{
    public abstract class Vivienda : IEquatable<Vivienda>, IActiveRecord
    {

        #region parameters
        private int id { get; set; }
        private String calle { get; set; }
        private int nro_puerta { get; set; }
        private Barrio barrio { get; set; }
        private String descripcion { get; set; }
        private int nro_banios { get; set; }
        private int nro_dormitorios { get; set; }
        private double superficie { get; set; }
        private double precio_base { get; set; }
        private int anio_construccion { get; set; }
        private Boolean vendida { get; set; } = false;
        private Boolean habilitada { get; set; } = false;
        #endregion


        public Boolean Eliminar()
        {
            throw new NotImplementedException();
        }

        public Boolean Equals(Vivienda other)
        {
            throw new NotImplementedException();
        }

        public Boolean Insertar()
        {
            throw new NotImplementedException();
        }

        public Boolean Modificar()
        {
            throw new NotImplementedException();
        }

        public virtual Boolean ValidarDireccion() {
            return calle != null && calle != "" && nro_puerta > 0;
        }

        public virtual Boolean Validar()
        {
            return barrio != null && descripcion != null && nro_banios >= 0 && nro_dormitorios > 0 && superficie > 0 && precio_base > 0 && anio_construccion > -10000;
        }

        public abstract double CalcularPrecio();

    }
}
