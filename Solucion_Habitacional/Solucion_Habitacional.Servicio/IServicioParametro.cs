using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Solucion_Habitacional.Servicio
{
    [ServiceContract]
    public interface IServicioParametro
    {
        [OperationContract]
        Boolean Agregar(String name, String value);

        [OperationContract]
        Boolean Modificar(String name, String value);

        [OperationContract]
        Boolean Eliminar(String name);

        [OperationContract]
        IEnumerable<DtoParametro> ObtenerTodos();

        [OperationContract]
        DtoParametro GetParametro(String name);

        [OperationContract]
        Boolean GenerateReport();
    }

    [DataContract]
    public class DtoParametro
    {

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String value { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return name + "=" + value;
        }
    }
}
