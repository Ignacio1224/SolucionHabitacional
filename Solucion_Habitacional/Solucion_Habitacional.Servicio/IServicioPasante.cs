using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Solucion_Habitacional.Servicio
{

    [ServiceContract]
    public interface IServicioPasante
    {
        [OperationContract]
        Boolean Agregar(String user_name, String password);

        [OperationContract]
        Boolean Modificar(String user_name, String password);

        [OperationContract]
        Boolean Eliminar(DtoPasante p);

        [OperationContract]
        Boolean Ingresar(DtoPasante p);

        [OperationContract]
        IEnumerable<DtoPasante> ObtenerTodos();

        [OperationContract]
        DtoPasante GetPasante(String user_name);
    }

    [DataContract]
    public class DtoPasante
    {

        [DataMember]
        public String user_name { get; set; }

        [DataMember]
        public String password { get; set; }
    }
}
