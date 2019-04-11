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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    valor = CompleteField("Valor", false);

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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    valor = CompleteField("Valor", false);

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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    deleted = repoParametro.Delete(new Parametro
                    {
                        nombre = name,
                        valor = null
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
                name = CompleteField("Nombre", true);

                canceled = name == "";

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
                    user_name = CompleteField("Email", true);

                    canceled = user_name == "";

                    if (canceled)
                    {
                        CanceledOperation();
                    }
                    else
                    {
                        password = CompleteField("Contraseña", false);

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
                name = CompleteField("Email", true);
                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    password1 = CompleteField("Contraseña", false);
                    password2 = CompleteField("Contraseña", false);

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

                password1 = CompleteField("Contraseña", true);

                canceled = password1 == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    password2 = CompleteField("Contraseña", false);

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
                password = CompleteField("Contraseña", true);

                canceled = password == "";

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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = CompleteField("Descripcion", false);

                    added = repoBarrio.Add(new Barrio
                    {
                        nombre = name,
                        descripcion = desc
                    });

                    EvaluateOperation(added, "Ingreso", "barrio", "nombre", false, true, true, true);

                }
            }
        }

        private static void ListarBarrios()
        {
            Console.WriteLine("\nListar Barrios");
            var lista = repoBarrio.FindAll();
            MostrarLista(lista);
            PararPantalla();
        }

        private static void ListarBarrio()
        {
            Boolean founded = false, canceled = false;
            String name = "";
            Barrio b = null;

            while (!founded && !canceled && b == null)
            {
                name = CompleteField("Nombre", true);

                canceled = name == "";

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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = CompleteField("Descripcion", false);

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
                name = CompleteField("Nombre", true);

                canceled = name == "";

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
            String name = "", desc = "";

            Console.WriteLine("\nAgregar un Barrio");

            while (!added && !canceled)
            {
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = CompleteField("Descripcion", false);

                    added = repoBarrio.Add(new Barrio
                    {
                        nombre = name,
                        descripcion = desc
                    });

                    EvaluateOperation(added, "Ingreso", "barrio", "nombre", false, true, true, true);

                }
            }
        }

        private static void ListarViviendas()
        {
            Console.WriteLine("\nListar Barrios");
            var lista = repoBarrio.FindAll();
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
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    b = repoBarrio.FindByName(name);

                    if (b != null)
                    {
                        Console.WriteLine("\n\tSe ha encontrado 1 barrio: " + b.ToString());
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
            String name = "", desc = "";

            Console.WriteLine("\nModificar Barrio");

            while (!modified && !canceled)
            {
                name = CompleteField("Nombre", true);

                canceled = name == "";

                if (canceled)
                {
                    CanceledOperation();
                }
                else
                {
                    desc = CompleteField("Descripcion", false);

                    modified = repoBarrio.Update(new Barrio
                    {
                        nombre = name,
                        descripcion = desc
                    });

                    EvaluateOperation(modified, "Modificación", "barrio", "nombre", true, false, true, true);

                }
            }

        }

        private static void EliminarVivienda()
        {
            Boolean deleted = false, canceled = false;
            String name = "";

            Console.WriteLine("\nEliminar un Barrio");

            while (!deleted && !canceled)
            {
                name = CompleteField("Nombre", true);

                canceled = name == "";

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

        private static void GenerarReporteVivienda()
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
                Console.WriteLine("11 - Buscar Vivienda por Nombre");
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
            int i = 0;
            foreach (Object o in lista)
            {
                i++;
                Console.WriteLine("\t" + i + " --> " + o.ToString());
            }
        }

        private static Boolean Salir()
        {
            Console.WriteLine("\n\nSalir");
            PararPantalla();
            DibujarMenu();
            return true;
            //Environment.Exit(0);
        }

        private static String CompleteField(String message, Boolean salir)
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

                if (salir && field == "0") return "";

                if (field == null || field == "")
                {
                    Console.WriteLine("\n\nOooops\n\tEl campo no puede estar vacío\n\tIngréselo nuevamente");
                }
            }
            return field;
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
