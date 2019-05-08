using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Solucion_Habitacional.Servicio
{
    [ServiceContract]
    public interface IServicioVivienda
    {
        [OperationContract]
        Boolean Agregar(String calle, int nro_puerta, DtoBarrio barrio, String descripcion, 
            int nro_banios, int nro_dormitorios, double superficie, double precio_base, 
            int anio_construccion, Boolean habilitada, Boolean vendida, Boolean nueva);


        [OperationContract]
        IEnumerable<DtoVivienda> FindAll();

        [OperationContract]
        IEnumerable<DtoVivienda> GetViviendas(DtoBarrio b);

        [OperationContract]
        Boolean GenerateReport();

        [OperationContract]
        Boolean Modificar(DtoVivienda v);

        [OperationContract]
        Boolean Eliminar(DtoVivienda v);

        [OperationContract]
        DtoVivienda FindById(int id);
    }

    [DataContract]
    public class DtoVivienda
    {
        #region parameters

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String calle { get; set; }

        [DataMember]
        public int nro_puerta { get; set; }

        [DataMember]
        public DtoBarrio barrio { get; set; }

        [DataMember]
        public String descripcion { get; set; }

        [DataMember]
        public int nro_banios { get; set; }

        [DataMember]
        public int nro_dormitorios { get; set; }

        [DataMember]
        public double superficie { get; set; }

        [DataMember]
        public double precio_base { get; set; }

        [DataMember]
        public int anio_construccion { get; set; }

        [DataMember]
        public Boolean vendida { get; set; } = false;

        [DataMember]
        public Boolean habilitada { get; set; } = false;

        [DataMember]
        public int tipo { get; set; }
        #endregion

        [OperationContract]
        public override string ToString()
        {
            return "\n\t\t" + "Id: " + id + "\n\t\t" + "Dirección: " + calle + " " + nro_puerta + "\n\t\t" + "Barrio: " + barrio.name + "\n\t\t"
                + "Descripción: " + descripcion + "\n\t\t" + "Cantidad de baños: " + nro_banios + "\n\t\t"
                + "Cantidad de dormitorios: " + nro_dormitorios + "\n\t\t" + "Superficie: " + superficie + "\n\t\t" + "Precio base: "
                + precio_base + "\n\t\t" + "Año de construcción: " + anio_construccion + "\n\t\t" + "Vendida: " + (vendida ? "Si" : "No") + "\n\t\t"
                + "Habilitada: " + (habilitada ? "Si" : "No");
        }

    }

    [DataContract]
    public class DtoVNueva : DtoVivienda
    {
        [DataMember]
        public double precio_final { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return base.ToString()
                + "\n\t\t" + "Precio final: " + precio_final;
        }

    }

    [DataContract]
    public class DtoVUsada : DtoVivienda
    {
        [DataMember]
        public double contribucion { get; set; }

        [DataMember]
        public double precio_final { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return base.ToString() + "\n\t\t" + "Contribución: " + contribucion + "\n\t\t" + "Precio final: " + precio_final;
        }

    }


}
