using Solucion_Habitacional.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Solucion_Habitacional.Servicio
{
    public class ObjectConversor
    {

        #region Barrio
        public static Barrio ConvertToBarrio(DtoBarrio dto_barrio)
        {
            Barrio b = null;

            if (dto_barrio != null)
            {
                b = new Barrio {
                    nombre = dto_barrio.name,
                    descripcion = dto_barrio.description
                };
                
            }

            return b;
        }

        public static DtoBarrio ConvertToDtoBarrio(Barrio barrio)
        {
            DtoBarrio dto_barrio = null;

            if (barrio != null)
            {
                dto_barrio = new DtoBarrio
                {
                    name = barrio.nombre,
                    description = barrio.descripcion
                };
            }

            return dto_barrio;
        }

        public static IEnumerable<DtoBarrio> ConvertToDtoBarrio(IEnumerable<Barrio> barrios)
        {
            List<DtoBarrio> dto_barrios = new List<DtoBarrio>();

            if (barrios.Count() > 0)
            {
                foreach (Barrio b in barrios) {
                    dto_barrios.Add(new DtoBarrio
                    {
                        name = b.nombre,
                        description = b.descripcion
                    });
                }
            }

            return dto_barrios;
        }
        #endregion


        #region Parametro

        public static Parametro ConvertToParametro(DtoParametro dto_parametro)
        {
            Parametro p = null;

            if (dto_parametro != null)
            {
                p = new Parametro
                {
                    nombre = dto_parametro.name,
                    valor = dto_parametro.value
                };

            }

            return p;
        }

        public static DtoParametro ConvertToDtoParametro(Parametro p)
        {
            DtoParametro dto_parametro = null;

            if (p != null)
            {
                dto_parametro = new DtoParametro
                {
                    name = p.nombre,
                    value = p.valor
                };
            }

            return dto_parametro;
        }

        public static IEnumerable<DtoParametro> ConvertToDtoParametro(IEnumerable<Parametro> parametros)
        {
            List<DtoParametro> dto_parametros = new List<DtoParametro>();

            if (parametros.Count() > 0)
            {
                foreach (Parametro p in parametros)
                {
                    dto_parametros.Add(new DtoParametro
                    {
                        name = p.nombre,
                        value = p.valor
                    });
                }
            }

            return dto_parametros;
        }

        #endregion


        #region Pasante

        public static Pasante ConvertToPasante(DtoPasante dto_pasante)
        {
            Pasante p = null;

            if (dto_pasante != null)
            {
                p = new Pasante
                {
                    user_name = dto_pasante.user_name,
                    password = dto_pasante.password
                };

            }

            return p;
        }

        public static DtoPasante ConvertToDtoPasante(Pasante p)
        {
            DtoPasante dto_pasante = null;

            if (p != null)
            {
                dto_pasante = new DtoPasante
                {
                    user_name = p.user_name,
                    password = p.password
                };
            }

            return dto_pasante;
        }

        public static IEnumerable<DtoPasante> ConvertToDtoPasante(IEnumerable<Pasante> pasantes)
        {
            List<DtoPasante> dto_pasantes = new List<DtoPasante>();

            if (pasantes.Count() > 0)
            {
                foreach (Pasante p in pasantes)
                {
                    dto_pasantes.Add(new DtoPasante
                    {
                        user_name = p.user_name,
                        password = p.password
                    });
                }
            }

            return dto_pasantes;
        }

        #endregion


        #region Vivienda

        public static Vivienda ConvertToVivienda(DtoVivienda dto_vivienda)
        {
            Vivienda v = null;

            if (dto_vivienda != null)
            {
                if (dto_vivienda.tipo == 0)
                {
                    DtoVNueva vn = (DtoVNueva)dto_vivienda;

                    v = new VNueva
                    {
                        calle = vn.calle,
                        nro_puerta = vn.nro_puerta,
                        barrio = ConvertToBarrio(vn.barrio),
                        descripcion = vn.descripcion,
                        nro_banios = vn.nro_banios,
                        nro_dormitorios = vn.nro_dormitorios,
                        superficie = vn.superficie,
                        precio_base = vn.precio_base,
                        anio_construccion = vn.anio_construccion,
                        vendida = vn.vendida,
                        habilitada = vn.habilitada,
                        precio_final = vn.precio_final,
                        tipo = vn.tipo,
                        id = vn.id
                    };
                } else if (dto_vivienda.tipo == 1)
                {
                    DtoVUsada vu = (DtoVUsada)dto_vivienda;

                    v = new VUsada
                    {
                        calle = vu.calle,
                        nro_puerta = vu.nro_puerta,
                        barrio = ConvertToBarrio(vu.barrio),
                        descripcion = vu.descripcion,
                        nro_banios = vu.nro_banios,
                        nro_dormitorios = vu.nro_dormitorios,
                        superficie = vu.superficie,
                        precio_base = vu.precio_base,
                        anio_construccion = vu.anio_construccion,
                        vendida = vu.vendida,
                        habilitada = vu.habilitada,
                        precio_final = vu.precio_final,
                        tipo = vu.tipo,
                        contribucion = vu.contribucion,
                        id = vu.id
                    };
                }
                

            }

            return v;
        }

        public static DtoVivienda ConvertToDtoVivienda(Vivienda v)
        {
            DtoVivienda dto_vivienda = null;

            if (v != null)
            {
                if (v.tipo == 0)
                {
                    VNueva vn = (VNueva)v;

                    dto_vivienda = new DtoVNueva
                    {
                        calle = vn.calle,
                        nro_puerta = vn.nro_puerta,
                        barrio = ConvertToDtoBarrio(vn.barrio),
                        descripcion = vn.descripcion,
                        nro_banios = vn.nro_banios,
                        nro_dormitorios = vn.nro_dormitorios,
                        superficie = vn.superficie,
                        precio_base = vn.precio_base,
                        anio_construccion = vn.anio_construccion,
                        vendida = vn.vendida,
                        habilitada = vn.habilitada,
                        precio_final = vn.precio_final,
                        tipo = vn.tipo,
                        id = vn.id
                    };
                } else if (v.tipo == 1)
                {
                    VUsada vu = (VUsada)v;

                    dto_vivienda = new DtoVUsada
                    {
                        calle = vu.calle,
                        nro_puerta = vu.nro_puerta,
                        barrio = ConvertToDtoBarrio(vu.barrio),
                        descripcion = vu.descripcion,
                        nro_banios = vu.nro_banios,
                        nro_dormitorios = vu.nro_dormitorios,
                        superficie = vu.superficie,
                        precio_base = vu.precio_base,
                        anio_construccion = vu.anio_construccion,
                        vendida = vu.vendida,
                        habilitada = vu.habilitada,
                        precio_final = vu.precio_final,
                        tipo = vu.tipo,
                        contribucion = vu.contribucion,
                        id = vu.id
                    };
                }

            }

            return dto_vivienda;
        }

        public static IEnumerable<DtoVivienda> ConvertToDtoVivienda(IEnumerable<Vivienda> viviendas)
        {
            List<DtoVivienda> dto_viviendas = new List<DtoVivienda>();

            if (viviendas.Count() > 0)
            {
                foreach (Vivienda v in viviendas)
                {
                    if (v.tipo == 0)
                    {
                        VNueva vn = (VNueva)v;
                        dto_viviendas.Add(new DtoVNueva
                        {
                            calle = vn.calle,
                            nro_puerta = vn.nro_puerta,
                            barrio = ConvertToDtoBarrio(vn.barrio),
                            descripcion = vn.descripcion,
                            nro_banios = vn.nro_banios,
                            nro_dormitorios = vn.nro_dormitorios,
                            superficie = vn.superficie,
                            precio_base = vn.precio_base,
                            anio_construccion = vn.anio_construccion,
                            vendida = vn.vendida,
                            habilitada = vn.habilitada,
                            precio_final = vn.precio_final,
                            tipo = vn.tipo,
                            id = vn.id
                        });
                    } else if (v.tipo == 1)
                    {
                        VUsada vu = (VUsada)v;
                        dto_viviendas.Add(new DtoVUsada
                        {
                            calle = vu.calle,
                            nro_puerta = vu.nro_puerta,
                            barrio = ConvertToDtoBarrio(vu.barrio),
                            descripcion = vu.descripcion,
                            nro_banios = vu.nro_banios,
                            nro_dormitorios = vu.nro_dormitorios,
                            superficie = vu.superficie,
                            precio_base = vu.precio_base,
                            anio_construccion = vu.anio_construccion,
                            vendida = vu.vendida,
                            habilitada = vu.habilitada,
                            precio_final = vu.precio_final,
                            tipo = vu.tipo,
                            contribucion = vu.contribucion,
                            id = vu.id
                        });
                    }
                }
            }

            return dto_viviendas;
        }

        #endregion

    }
}
