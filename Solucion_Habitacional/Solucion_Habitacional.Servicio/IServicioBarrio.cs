using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Solucion_Habitacional.Servicio
{

    [ServiceContract]
    public interface IServicioBarrio
    {
        [OperationContract]
        Boolean Agregar(String name, String description);

        [OperationContract]
        Boolean Eliminar(String name);

        [OperationContract]
        Boolean Modificar(String name, String description);

        [OperationContract]
        IEnumerable<DtoBarrio> ObtenerTodos();

        [OperationContract]
        DtoBarrio GetBarrio(String name);

        [OperationContract]
        Boolean GenerateReport();
    }


    [DataContract]
    public class DtoBarrio
    {

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public String description { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return "Nombre: " + name + " - Descripción: " + description;
        }
    }
}
