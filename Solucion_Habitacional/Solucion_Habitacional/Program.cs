using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solucion_Habitacional.Dominio;
using Solucion_Habitacional.Dominio.Repositorios.ADO;


namespace Solucion_Habitacional
{
    class Program
    {
        static RepositorioBarrio repoBarrio = new RepositorioBarrio();
        static RepositorioPasante repoPasante = new RepositorioPasante();
        static RepositorioParametro repoParametro = new RepositorioParametro();
        static RepositorioVivienda repoVivienda = new RepositorioVivienda();

        static Boolean autenticado = false;
        static Pasante pasante = null;

        static void Main(string[] args)
        {
            int opcion = -1;
            do
            {
                DibujarMenu();

                opcion = LeerOpcion();

                if (opcion != 0)
                    ProcesarMenu(opcion);

            } while (opcion != 0);

            Console.WriteLine("\nFin del programa.");

            PararPantalla();
        }

        #region PARAMETRO

        private static void AgregarParametro()
        {
            Boolean added = false, canceled = false;
            String name = "", valor = "";

            Console.WriteLine("\nAgregar un Parámetro");

            while (!added && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    valor = (String) CompleteField("Valor", false);

                    added = repoParametro.Add(new Parametro
                    {
                        nombre = name,
                        valor = valor
                    });

                    EvaluateOperation(added, "Ingreso", "parámetro", "nombre", false, true, true, true);

                }
            }
        }

        private static void ModificarParametro()
        {
            Boolean modified = false, canceled = false;
            String name = "", valor = "";

            Console.WriteLine("\nModificar Parámetro");

            while (!modified && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    valor = (String) CompleteField("Valor", false);

                    modified = repoParametro.Update(new Parametro
                    {
                        nombre = name,
                        valor = valor
                    });

                    EvaluateOperation(modified, "Modificación", "parámetro", "nombre", true, false, true, true);

                }
            }
        }

        private static void EliminarParametro()
        {
            Boolean deleted = false, canceled = false;
            String name = "";

            Console.WriteLine("\nEliminar un Parámetro");

            while (!deleted && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    deleted = repoParametro.Delete(new Parametro
                    {
                        nombre = name,
                        valor = ""
                    });

                    EvaluateOperation(deleted, "Eliminación", "parámetro", "nombre", true, false, true, true);

                }
            }
        }

        private static void ListarParametros()
        {
            Console.WriteLine("\nListar Parametros");
            var lista = repoParametro.FindAll();
            MostrarLista(lista);
            PararPantalla();
        }

        private static void ListarParametro()
        {
            Boolean founded = false, canceled = false;
            String name = "";
            Parametro p = null;

            while (!founded && !canceled && p == null)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    p = repoParametro.FindByName(name);

                    if (p != null)
                    {
                        Console.WriteLine("\n\tSe ha encontrado 1 parámetro: " + p.ToString());
                    }
                    else
                    {
                        Console.WriteLine("\n\tNo se ha encontrado nungún parámetro con el nombre: " + name);
                    }

                    PararPantalla();
                }
            }
        }

        private static void GenerarReporteParametro()
        {
            Console.WriteLine("\nGenerar Reporte de Parametros");
            Boolean generado = repoParametro.GenerateReports();

            if (generado)
            {
                Console.WriteLine("\nGenerado exitosamente!");
            }
            else
            {
                Console.WriteLine("\nOooops! Ha ocurrido un error");
            }

            PararPantalla();
        }

        #endregion

        #region PASANTE
        private static void Ingresar(Boolean salt = false)
        {
            Boolean canceled = false;
            String user_name = "", password = "";

            Console.WriteLine("\nIngresar");

            while (!autenticado && !canceled)
            {
                if (pasante == null)
                {
                    user_name = (String) CompleteField("Email", true);

                    canceled = user_name == "-1";

                    if (canceled)
                    {
                        CanceledOperation();
                    }
                    else
                    {
                        password = (String) CompleteField("Contraseña", false);

                        pasante = new Pasante
                        {
                            user_name = user_name,
                            password = password
                        };
                    }
                }
                
                if (pasante != null)
                {
                    autenticado = repoPasante.Ingresar(pasante);

                    if (!autenticado)
                    {
                        pasante = null;
                    }

                    if (!salt)
                    {
                        EvaluateOperation(autenticado, "Ingreso", "usuario", "nombre de usuario", true, true, true, true);
                    }
                }
            }
        }

        private static void AgregarPasante()
        {
            Boolean added = false, canceled = false;
            String name = "", password1 = "a", password2 = "b";

            Console.WriteLine("\nRegistrarme");


            while (!added && !canceled && password1 != password2)
            {
                name = (String) CompleteField("Email", true);
                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    password1 = (String) CompleteField("Contraseña", false);
                    password2 = (String) CompleteField("Contraseña", false);

                    if (password1 != password2)
                    {
                        Console.WriteLine("\nLas contrasenas no coinciden, intente de nuevo!\n\n");
                    }
                    else
                    {
                        pasante = new Pasante
                        {
                            user_name = name,
                            password = password2
                        };

                        added = repoPasante.Add(pasante);

                        EvaluateOperation(added, "Registro", "usuario", "email", false, true, true, true);

                        if (!added)
                        {
                            password1 = "a";
                            password2 = "b";
                        } else
                        {
                            Ingresar(true);
                        }
                    }
                }
            }
        }

        private static void ModificarPasante()
        {
            Boolean modified = false, canceled = false;
            String password1 = "a", password2 = "b";


            Console.WriteLine("\nCambiar Contrasena");
            
            while (!modified && !canceled && password1 != password2)
            {

                password1 = (String) CompleteField("Contraseña", true);

                canceled = password1 == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    password2 = (String) CompleteField("Contraseña", false);

                    if (password1 != password2)
                    {
                        Console.WriteLine("\nLas contrasenas no coinciden, intente de nuevo!\n\n");
                    }
                    else
                    {
                        pasante = new Pasante
                        {
                            user_name = pasante.user_name,
                            password = password2
                        };

                        modified = repoPasante.Update(pasante);

                        if (!modified)
                        {
                            password1 = "a";
                            password2 = "b";
                        }

                        EvaluateOperation(modified, "Modificación", "usuario", "contraseña", true, false, true, false);

                    }
                }
            }

        }

        private static void EliminarPasante()
        {
            Boolean deleted = false, canceled = false;
            String password = "";

            Console.WriteLine("\nDarme de Baja");

            while (!deleted && !canceled)
            {
                password = (String) CompleteField("Contraseña", true);

                canceled = password == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    pasante = new Pasante
                    {
                        user_name = pasante.user_name,
                        password = password
                    };

                    deleted = repoPasante.Delete(pasante);

                    EvaluateOperation(deleted, "Eliminación", "usuario", "contrsaeña", true, false, true, false);

                    if (deleted)
                    {
                        autenticado = false;
                        pasante = null;
                    }

                }
            }
        }
        #endregion

        #region BARRIO
        private static void AgregarBarrio()
        {
            Boolean added = false, canceled = false;
            String name = "", desc = "";

            Console.WriteLine("\nAgregar un Barrio");

            while (!added && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = (String) CompleteField("Descripcion", false);

                    added = repoBarrio.Add(new Barrio
                    {
                        nombre = name,
                        descripcion = desc
                    });

                    EvaluateOperation(added, "Ingreso", "barrio", "nombre", false, true, true, true);

                }
            }
        }

        private static void ListarBarrios(Boolean cont = true)
        {
            Console.WriteLine("\nListar Barrios");
            var lista = repoBarrio.FindAll();
            MostrarLista(lista);

            if (cont)
            {
                PararPantalla();
            }
        }

        private static void ListarBarrio()
        {
            Boolean founded = false, canceled = false;
            String name = "";
            Barrio b = null;

            while (!founded && !canceled && b == null)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                } else
                {
                    b = repoBarrio.FindByName(name);

                    if (b != null)
                    {
                        Console.WriteLine("\n\tSe ha encontrado 1 barrio: " + b.ToString());
                    } else
                    {
                        Console.WriteLine("\n\tNo se ha encontrado nungún barrio con el nombre: " + name);
                    }
                    
                    PararPantalla();
                    
                }
            }

        }

        private static void ModificarBarrio()
        {
            Boolean modified = false, canceled = false;
            String name = "", desc = "";

            Console.WriteLine("\nModificar Barrio");

            while (!modified && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = (String) CompleteField("Descripcion", false);

                    modified = repoBarrio.Update(new Barrio
                    {
                        nombre = name,
                        descripcion = desc
                    });

                    EvaluateOperation(modified, "Modificación", "barrio", "nombre", true, false, true, true);

                }
            }

        }

        private static void EliminarBarrio()
        {
            Boolean deleted = false, canceled = false;
            String name = "";

            Console.WriteLine("\nEliminar un Barrio");

            while (!deleted && !canceled)
            {
                name = (String) CompleteField("Nombre", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    deleted = repoBarrio.Delete(new Barrio
                    {
                        nombre = name,
                        descripcion = null
                    });

                    EvaluateOperation(deleted, "Eliminación", "barrio", "nombre", true, false, true, true);

                }
            }
        }

        private static void GenerarReporteBarrio()
        {
            Console.WriteLine("\nGenerar Reporte de Barrios");
            Boolean generado = repoBarrio.GenerateReports();

            if (generado)
            {
                Console.WriteLine("\nGenerado exitosamente!");
            }
            else
            {
                Console.WriteLine("\nOooops! Ha ocurrido un error");
            }

            PararPantalla();
        }

        #endregion

        #region VIVIENDA
        private static void AgregarVivienda()
        {
            Boolean added = false, canceled = false;

            int current_year = DateTime.Now.Year;
            int anio_es_nueva = Convert.ToInt16(repoParametro.FindByName("anio_nueva").valor) | 2;

            String calle = "", descripcion = "";
            Barrio barrio = null;
            int nro_puerta = 0, nro_banios = 0, nro_dormitorios = 0, anio_construccion = 0;
            double superficie = 0.0, precio_base = 0.0;
            Boolean vendida = false, habilitada = false, intenta_ser_nueva = false;

            Console.WriteLine("\nAgregar una Vivienda");

            while (!added && !canceled)
            {
                calle = (String) CompleteField("Calle", true);
                canceled = calle == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                   
                    nro_puerta = Convert.ToInt32(CompleteField("Número de puerta", false, "Int"));

                    nro_banios = Convert.ToInt32(CompleteField("Cantidad de baños", false, "Int"));

                    nro_dormitorios = Convert.ToInt32(CompleteField("Cantidad de dormitorios", false, "Int"));

                    anio_construccion = Convert.ToInt32(CompleteField("Año de construcción", false, "Int"));

                    superficie = Convert.ToDouble(CompleteField("Superficie en m2", false, "Double"));

                    intenta_ser_nueva = current_year - anio_construccion < anio_es_nueva;

                    if (intenta_ser_nueva)
                    {
                        Console.Write("\nEl precio es en Unidades Indexadas");
                    } else
                    {
                        Console.Write("\nEl precio es en Dólares");
                    }

                    precio_base = Convert.ToDouble(CompleteField("Precio base", false, "Double"));

                    habilitada = Convert.ToBoolean(CompleteField("Está habilitada? [s --> Si, n --> No]", false, "Boolean"));

                    if (habilitada)
                    {
                        vendida = Convert.ToBoolean(CompleteField("Está vendida? [s --> Si, n --> No]", false, "Boolean"));
                    }

                    descripcion = (String)CompleteField("Descripcion", false);

                    barrio = (Barrio) CompleteField("Nombre del barrio", false, "Barrio");

                    if (intenta_ser_nueva)
                    {
                        added = AddVNueva(calle, nro_puerta, barrio, descripcion, nro_banios,
                                nro_dormitorios, superficie, precio_base, anio_construccion, vendida,
                                habilitada);

                        if (!added)
                        {
                            added = AddVNueva(calle, nro_puerta, barrio, descripcion, nro_banios,
                                nro_dormitorios, superficie, precio_base, anio_construccion, vendida,
                                habilitada);
                        }
                    } else
                    {
                        added = AddUsada(calle, nro_puerta, barrio, descripcion, nro_banios,
                                nro_dormitorios, superficie, precio_base, anio_construccion, vendida,
                                habilitada);

                        if (!added)
                        {
                            added = AddVNueva(calle, nro_puerta, barrio, descripcion, nro_banios,
                                nro_dormitorios, superficie, precio_base, anio_construccion, vendida, 
                                habilitada);
                        }
                    }

                    if (added)
                    {
                        Console.WriteLine("Ingreso correcto");
                    } else
                    {
                        Console.WriteLine("Ingreso incorrecto, Vuelva a intentarlo");
                    }

                    PararPantalla();
                }
            }
        }

        private static Boolean AddVNueva(String calle, int nro_puerta, Barrio barrio, 
            String descripcion, int nro_banios, int nro_dormitorios, 
            double superficie, double precio_base, int anio_construccion,
            Boolean vendida, Boolean habilitada)
        {
            Boolean added = false;
            VNueva v = new VNueva
            {
                calle = calle,
                nro_puerta = nro_puerta,
                barrio = barrio,
                descripcion = descripcion,
                nro_banios = nro_banios,
                nro_dormitorios = nro_dormitorios,
                superficie = superficie,
                precio_base = precio_base,
                anio_construccion = anio_construccion,
                vendida = vendida,
                habilitada = habilitada
            };

            if (v.Es_Nueva())
            {
                added = repoVivienda.Add(v);
            }

            return added;
        }

        private static Boolean AddUsada(String calle, int nro_puerta, Barrio barrio,
            String descripcion, int nro_banios, int nro_dormitorios,
            double superficie, double precio_base, int anio_construccion,
            Boolean vendida, Boolean habilitada)
        {
            Boolean added = false;
            VUsada v = new VUsada
            {
                calle = calle,
                nro_puerta = nro_puerta,
                barrio = barrio,
                descripcion = descripcion,
                nro_banios = nro_banios,
                nro_dormitorios = nro_dormitorios,
                superficie = superficie,
                precio_base = precio_base,
                anio_construccion = anio_construccion,
                vendida = vendida,
                habilitada = habilitada
            };

            if (!v.Es_Nueva())
            {
                added = repoVivienda.Add(v);
            }

            return added;
        }

        private static void ListarViviendas()
        {
            Console.WriteLine("\nListar Barrios");
            var lista = repoVivienda.FindAll();
            MostrarLista(lista);
            PararPantalla();
        }

        private static void ListarVivienda()
        {
            Boolean founded = false, canceled = false;
            String name = "";
            Barrio b = null;

            

            while (!founded && !canceled && b == null)
            {
                name = (String) CompleteField("Nombre del barrio", true);

                canceled = name == "-1";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    b = repoBarrio.FindByName(name);

                    if (b != null)
                    {
                        MostrarLista(repoVivienda.FindByLocation(b));
                    }
                    else
                    {
                        Console.WriteLine("\n\tNo se ha encontrado nungún barrio con el nombre: " + name);
                    }

                    PararPantalla();
                }
            }

        }

        private static void ModificarVivienda()
        {
            Boolean modified = false, canceled = false;
            int id = -1;

            Console.WriteLine("\nModificar Vivienda");

            while (!modified && !canceled)
            {
                id = (int) CompleteField("Id", true, "Int");

                canceled = id == -1;

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    Vivienda v = repoVivienda.FindById(id);

                    int op = -1;

                    while (!canceled)
                    {

                        while (op < 0 || op > 11)
                        {
                            Console.WriteLine("\tCaracterística a modificar");
                            Console.WriteLine("\t[0] \t--> Salir");
                            Console.WriteLine("\t[1] \t--> Calle");
                            Console.WriteLine("\t[2] \t--> Número de puerta");
                            Console.WriteLine("\t[3] \t--> Barrio");
                            Console.WriteLine("\t[4] \t--> Descripción");
                            Console.WriteLine("\t[5] \t--> Cantidad de baños");
                            Console.WriteLine("\t[6] \t--> Cantidad de dormitorios");
                            Console.WriteLine("\t[7] \t--> Superficie");
                            Console.WriteLine("\t[8] \t--> Precio base");
                            Console.WriteLine("\t[9] \t--> Año de construcción");
                            Console.WriteLine("\t[10] \t--> Habilitación");
                            Console.WriteLine("\t[11] \t--> Vendida");

                            op = (int)CompleteField("Ingrese una opción", false, "Int");
                        }

                        if (op == 0)
                        {
                            canceled = true;
                            CanceledOperation();
                        }
                        else
                        {

                            switch (op)
                            {
                                case 1:
                                    String calle = (String)CompleteField("Calle", false);
                                    v.calle = calle;
                                    break;

                                case 2:
                                    int nro_puerta = (int)CompleteField("Número de puerta", false, "Int");
                                    v.nro_puerta = nro_puerta;
                                    break;

                                case 3:
                                    Barrio barrio = (Barrio)CompleteField("Barrio", false, "Barrio");
                                    v.barrio = barrio;
                                    break;

                                case 4:
                                    String descripcion = (String)CompleteField("Descripcion", false);
                                    v.descripcion = descripcion;
                                    break;

                                case 5:
                                    int nro_banios = (int)CompleteField("Cantidad de baños", false, "Int");
                                    v.nro_banios = nro_banios;
                                    break;

                                case 6:
                                    int nro_dormitorios = (int)CompleteField("Cantidad de doemitorios", false, "Int");
                                    v.nro_dormitorios = nro_dormitorios;
                                    break;

                                case 7:
                                    double superficie = (double)CompleteField("Superficie en m2", false, "Double");
                                    v.superficie = superficie;
                                    break;

                                case 8:
                                    double precio_base = (double)CompleteField("Precio base", false, "Double");
                                    v.precio_base = precio_base;
                                    break;

                                case 9:
                                    int anio_construccion = (int)CompleteField("Año de construcción", false, "Int");
                                    v.anio_construccion = anio_construccion;
                                    break;

                                case 10:
                                    Boolean habilitada = (Boolean) CompleteField("Habilitada?", false, "Boolean");
                                    v.habilitada = habilitada;
                                    break;

                                case 11:
                                    if (v.habilitada)
                                    {
                                        Boolean vendida = (Boolean)CompleteField("Vendida?", false, "Boolean");
                                        v.vendida = vendida;
                                    }
                                    else
                                    {
                                        Console.WriteLine("La vivienda no esta habilitada para la venta");
                                        PararPantalla();
                                    }
                                    break;
                            }

                            modified = repoVivienda.Update(v);


                            if (modified)
                            {
                                Console.WriteLine("Modificación correcta");
                            } else
                            {
                                Console.WriteLine("No se pudo modificar");
                            }

                            PararPantalla();

                            canceled = !(Boolean)CompleteField("Desea volver a modificar? [s --> Si, n --> No]", false, "Boolean");

                            op = canceled ? 0 : -1;
                        }
                    }
                }
            }

        }

        private static void EliminarVivienda()
        {
            Boolean deleted = false, canceled = false;
            int id = -1;

            Console.WriteLine("\nEliminar vivienda");

            while (!deleted && !canceled)
            {
                id = Convert.ToInt32(CompleteField("Id", true, "Int"));

                canceled = id == -1;

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    Vivienda v = repoVivienda.FindById(id);
                    deleted = repoVivienda.Delete(v);

                    EvaluateOperation(deleted, "Eliminación", "vivienda", "id", true, false, false, true);

                }
            }
        }

        private static void GenerarReporteVivienda()
        {
            Console.WriteLine("\nGenerar Reporte de Vivienda");
            Boolean generado = repoVivienda.GenerateReports();

            if (generado)
            {
                Console.WriteLine("\nGenerado exitosamente!");
            }
            else
            {
                Console.WriteLine("\nOooops! Ha ocurrido un error");
            }

            PararPantalla();
        }

        #endregion


        #region UTILITIES
        private static void DibujarMenu()
        {
            Console.Clear();

            if (!autenticado)
            {
                Console.WriteLine("Menú de Inicio");
                Console.WriteLine("=================");
                Console.WriteLine("0 - Salir");
                Console.WriteLine("1 - Ingresar");
                Console.WriteLine("2 - Registrarme");
            }
            else
            {
                Console.WriteLine("======== Menú de Opciones ========");
                Console.WriteLine("==================================");
                Console.WriteLine("0 - Salir");
                Console.WriteLine("============= BARRIO =============");
                Console.WriteLine("1 - Agregar Barrio");
                Console.WriteLine("2 - Modificar Barrio");
                Console.WriteLine("3 - Listar Barrios");
                Console.WriteLine("4 - Eliminar Barrio");
                Console.WriteLine("5 - Buscar Barrio por Nombre");
                Console.WriteLine("6 - Generar Reporte de Barrios");
                Console.WriteLine("==================================");
                Console.WriteLine("============ VIVIENDA ============");
                Console.WriteLine("7 - Agregar Vivienda");
                Console.WriteLine("8 - Modificar Vivienda");
                Console.WriteLine("9 - Listar Viviendas");
                Console.WriteLine("10 - Eliminar Vivienda");
                Console.WriteLine("11 - Buscar Vivienda por Barrio");
                Console.WriteLine("12 - Generar Reporte de Vivienda");
                Console.WriteLine("==================================");
                Console.WriteLine("============ PARAMETRO ===========");
                Console.WriteLine("13 - Agregar Parametro");
                Console.WriteLine("14 - Modificar Parametro");
                Console.WriteLine("15 - Listar Parametros");
                Console.WriteLine("16 - Eliminar Parametro");
                Console.WriteLine("17 - Buscar Parametro por Nombre");
                Console.WriteLine("18 - Generar Reporte de Parametros");
                Console.WriteLine("==================================");
                Console.WriteLine("============= PASANTE ============");
                Console.WriteLine("19 - Cambiar contrasena");
                Console.WriteLine("20 - Darme de Baja");
                Console.WriteLine("==================================");
            }

            Console.WriteLine("");
            Console.WriteLine("Ingrese una opción");


        }

        private static int LeerOpcion()
        {
            int tope = 20;
            int opcion = -1;
            bool esNumero = false;
            String opS;
            do
            {
                opS = Console.ReadLine();
                esNumero = int.TryParse(opS.ToString(), out opcion);
                if (!esNumero || opcion < 0 || opcion > tope)
                    Console.WriteLine("Ingrese nuevamente, la opción debe estar entre 0 y {0}", tope);
            } while (!esNumero || opcion < 0 || opcion > tope);

            return opcion;
        }

        private static void ProcesarMenu(int opcion)
        {

            switch (opcion)
            {
                case 1:
                    if (!autenticado)
                    {
                        Ingresar();
                    }
                    else
                    {
                        AgregarBarrio();
                    }
                    break;

                case 2:
                    if (!autenticado)
                    {
                        AgregarPasante();
                    }
                    else
                    {
                        ModificarBarrio();
                    }
                    break;

                case 3:
                    ListarBarrios();
                    break;

                case 4:
                    EliminarBarrio();
                    break;

                case 5:
                    ListarBarrio();
                    break;

                case 6:
                    GenerarReporteBarrio();
                    break;

                case 7:
                    AgregarVivienda();
                    break;

                case 8:
                    ModificarVivienda();
                    break;

                case 9:
                    ListarViviendas();
                    break;

                case 10:
                    EliminarVivienda();
                    break;

                case 11:
                    ListarVivienda();
                    break;

                case 12:
                    GenerarReporteVivienda();
                    break;

                case 13:
                    AgregarParametro();
                    break;

                case 14:
                    ModificarParametro();
                    break;

                case 15:
                    ListarParametros();
                    break;

                case 16:
                    EliminarParametro();
                    break;

                case 17:
                    ListarParametro();
                    break;

                case 18:
                    GenerarReporteParametro();
                    break;

                case 19:
                    ModificarPasante();
                    break;

                case 20:
                    EliminarPasante();
                    break;

            }

        }

        private static void PararPantalla()
        {
            Console.WriteLine("\nPresione una tecla para continuar");
            Console.ReadKey();
        }

        private static void MostrarLista(IEnumerable<Object> lista)
        {
            if (lista.Count() == 0)
            {
                Console.WriteLine("\t No se han encontrado los datos solicitados");
            } else
            {
                int i = 0;
                foreach (Object o in lista)
                {
                    i++;
                    Console.WriteLine("\t" + i + " --> " + o.ToString());
                }
            }
            
        }
        

        private static Object CompleteField(String message, Boolean salir)
        {
            String field = "";

            while (field == null || field == "")
            {

                if (salir)
                {
                    Console.WriteLine("[0 --> Salir]");
                }

                Console.WriteLine("\n" + message + ": ");
                field = Console.ReadLine();

                if (salir && field == "0")
                {
                    field = "-1";
                }
                else if (field == null || field == "")
                {
                    Console.WriteLine("\n\nOooops\n\tEl campo no puede estar vacío\n\tIngréselo nuevamente");
                }
            }
            return field;
        }

        private static Object CompleteField(String message, Boolean exit, String data_type)
        {
            Boolean converted = false;
            Object da = null;
            switch (data_type)
            {
                case "Int":
                    int i = 0;
                    while (!converted)
                    {
                        converted = int.TryParse((String)CompleteField(message, exit), out i);

                        if (!converted)
                        {
                            Console.WriteLine("\n\tEl dato no es válido\n\tIntente nuevamente!");
                        }
                    }
                    da = i;
                    break;

                case "Double":
                    double d = 0;
                    while (!converted)
                    {
                        converted = double.TryParse((String)CompleteField(message, exit), out d);

                        if (!converted)
                        {
                            Console.WriteLine("\n\tEl dato no es válido\n\tIntente nuevamente!");
                        }
                    }
                    da = d;
                    break;

                case "Boolean":
                    Boolean b = false;
                    String habilitado = "";

                    while (habilitado != "n" && habilitado != "N" && habilitado != "s" && habilitado != "S")
                    {
                        habilitado = (String) CompleteField(message, exit);

                        habilitado = (habilitado == "") ? "N" : habilitado;

                        if (habilitado != "n" && habilitado != "N" && habilitado != "s" && habilitado != "S")
                        {
                            Console.WriteLine("\n\tEl dato no es válido\n\tIntente nuevamente!");
                        }
                    }

                    b = (habilitado == "s" || habilitado == "S") ? true : !(habilitado == "n" || habilitado == "N");

                    da = b;

                    break;

                case "Barrio":
                    ListarBarrios(false);

                    Barrio ba = null;
                    
                    while (ba == null)
                    {
                        ba = repoBarrio.FindByName((String) CompleteField(message, exit));

                        if (ba == null)
                        {
                            Console.WriteLine("\n\tEl dato no es válido\n\tIntente nuevamente!");
                        }
                    }

                    da = ba;

                    break;
            }

            return da;
        }

        private static void CanceledOperation()
        {
            Console.Clear();
            Console.WriteLine("\nOperación cancelada!");
            PararPantalla();
        }

        private static void EvaluateOperation(Boolean op, String operation, String obj, String id, Boolean exists_obj, Boolean gen_op, Boolean gen_obj, Boolean gen_id)
        {
            if (op)
            {
                Console.WriteLine("\n" + operation + " correct" + (gen_op ? "o" : "a") + "!");
                PararPantalla();
            }
            else
            {
                Console.WriteLine("\n" + operation + " incorrect" + (gen_op ? "o" : "a") + ", asegurese de que" + (exists_obj ? " " : " no ") + "exista" + (gen_obj ? " un " : " una ") + obj + " con" + (gen_id ? " el mismo " : " la misma ") + id + " y vuelva a intentarlo");
                PararPantalla();
            }
        }

        #endregion
    }
}
