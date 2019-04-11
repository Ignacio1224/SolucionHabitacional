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


        #region PASANTE
        private static void Ingresar(Boolean salt = false)
        {
            Boolean canceled = false;
            String user_name = "", password = "";

            Console.WriteLine("\nIngresar");

            while (pasante == null && !canceled)
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
                if (!salt)
                {
                    EvaluateOperation(autenticado, "Ingreso", "usuario", "nombre de usuario", true, true, true, true);
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

        /* CODE TASK! Apply restructured logic to Modificar Pasante and Elimiar Pasante and Parameters */

        private static Boolean ModificarPasante()
        {
            Boolean modificado = false;
            String passw1 = "a";
            String passw2 = "b";


            Console.WriteLine("\nCambiar Contrasena");

            while (passw1 != passw2)
            {
                Console.WriteLine("\nContrasena: [0 --> Salir]");
                passw1 = Console.ReadLine();

                if (passw1 == "0") return Salir();


                Console.WriteLine("\nContrasena: ");
                passw2 = Console.ReadLine();

                if (passw1 != passw2)
                {
                    Console.WriteLine("\n\nLas contrasenas no coinciden, Intente de nuevo!");
                    PararPantalla();
                }
            }

            pasante = new Pasante
            {
                user_name = pasante.user_name,
                password = passw2
            };

            modificado = repoPasante.Update(pasante);

            if (modificado)
            {
                Console.WriteLine("\nModificacion correcta!");
                PararPantalla();
                DibujarMenu();
            }
            else
            {
                Console.WriteLine("\nIntente nuevamente, recuerde que el campo no puede estar vacio y debe superar los 7 caracteres.");
                PararPantalla();
                ModificarPasante();
            }

            return true;

        }

        private static Boolean EliminarPasante()
        {
            Boolean eliminado = false;
            Console.WriteLine("\nDarme de Baja");

            while (!eliminado)
            {
                Console.WriteLine("\nContrasena: [0 --> Salir]");
                String passw = Console.ReadLine();

                if (passw == "0") return Salir();


                pasante = new Pasante
                {
                    user_name = pasante.user_name,
                    password = passw
                };

                eliminado = repoPasante.Delete(pasante);

                if (!eliminado)
                {
                    Console.WriteLine("\n\nLa contrasena almacendada no coincide con la ingresada, Intente de nuevo!");
                    PararPantalla();
                }
            }

            pasante = null;
            autenticado = false;
            Console.WriteLine("Eliminacion correcta");
            PararPantalla();
            DibujarMenu();
            return true;
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

                    EvaluateOperation(modified, "Modificación", "barrio", "nombre", true, true, true, true);

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

                    EvaluateOperation(deleted, "Eliminación", "barrio", "nombre", true, true, true, true);

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

        #region Parametro

        private static Boolean AgregarParametro()
        {
            Console.WriteLine("\nAgregar un Parametro");
            Console.WriteLine("\nNombre: [0 --> Salir]");
            String name = Console.ReadLine();

            if (name == "0") return Salir();

            Console.WriteLine("\nValor: ");
            String valor = Console.ReadLine();
            repoParametro.Add(new Parametro
            {
                nombre = name,
                valor = valor
            });

            PararPantalla();
            return true;
        }

        private static Boolean ModificarParametro()
        {
            Boolean modified = false;
            String nombre = null;
            String valor = null;

            Console.WriteLine("\nModificar Parametro");

            Console.WriteLine("\nNombre: [0 --> Salir]");
            nombre = Console.ReadLine();

            Console.WriteLine("\nValor:");
            valor = Console.ReadLine();

            if (valor == "0") return Salir();

            if (nombre == "" || valor == "")
            {
                Console.WriteLine("\n\nLos campos no pueden estar vacios, Intente de nuevo!");
                PararPantalla();
                ModificarParametro();
            }

            modified = repoParametro.Update(new Parametro
            {
                nombre = nombre,
                valor = valor
            });

            if (modified)
            {
                Console.WriteLine("\nModificado Correctamente!");
                PararPantalla();
                DibujarMenu();
            }
            else
            {
                Console.WriteLine("\nOoops! \n\tHa ocurrido un error, asegurate que el parametro exista, Intente nuevamente!");
                PararPantalla();
                ModificarParametro();
            }

            return true;
        }

        private static Boolean EliminarParametro()
        {
            Boolean deleted = false;

            Console.WriteLine("\nEliminar un Parametro");

            while (!deleted)
            {
                Console.WriteLine("\nNombre: [0 --> Salir]");
                String name = Console.ReadLine();

                if (name == "0") return Salir();

                deleted = repoParametro.Delete(new Parametro
                {
                    nombre = name,
                    valor = null
                });

                if (!deleted)
                {
                    Console.WriteLine("El parametro no se pudo eliminar, verifique que exista");
                    PararPantalla();
                    EliminarParametro();
                }
                else
                {
                    Console.WriteLine("El parametro se ha eliminado!");
                    PararPantalla();
                }
            }

            return true;
        }

        private static void ListarParametros()
        {
            Console.WriteLine("\nListar Parametros");
            var lista = repoParametro.FindAll();
            MostrarLista(lista);
            PararPantalla();
        }

        private static Boolean ListarParametro()
        {
            return true;
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

                case 6:
                    GenerarReporteBarrio();
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
