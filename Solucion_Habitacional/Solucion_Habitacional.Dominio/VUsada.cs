using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion_Habitacional.Dominio
{
    public class VUsada : Vivienda
    {
        private double contribucion { get; set; }
        public override double CalcularPrecio()
        {
            throw new NotImplementedException();
        }

    }
}
