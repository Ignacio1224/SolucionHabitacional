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
        private static Boolean Ingresar()
        {
            String msj;
            String username;
            String passw;

            if (pasante == null)
            {
                Console.WriteLine("\nIngresar");

                Console.WriteLine("\nUsuario: [0 --> Salir]");
                username = Console.ReadLine();

                if (username == "0") return Salir();

                Console.WriteLine("\nContrasena: ");
                passw = Console.ReadLine();

                pasante = new Pasante
                {
                    user_name = username,
                    password = passw
                };
            }

            autenticado = repoPasante.Ingresar(pasante);

            if (autenticado)
            {
                Console.WriteLine("Ingreso correcto");
                PararPantalla();
                DibujarMenu();
            }
            else
            {
                pasante = null;
                Console.WriteLine("Intente nuevamente, recuerde que los campos no pueden estar vacios.");
                PararPantalla();
                Ingresar();
            }
            return true;
        }

        private static Boolean AgregarPasante()
        {
            Boolean added = false;
            String passw1 = "a";
            String passw2 = "b";
            String user_name = "";

            Console.WriteLine("\nRegistrarme");

            Console.WriteLine("\nEmail: [0 --> Salir]");
            user_name = Console.ReadLine();

            if (user_name == "0") return Salir();

            while (passw1 != passw2)
            {
                Console.WriteLine("\nContrasena: ");
                passw1 = Console.ReadLine();
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
                user_name = user_name,
                password = passw2
            };

            added = repoPasante.Add(pasante);

            if (added)
            {
                Console.WriteLine("\nRegistro correcto!");
                Ingresar();
            }
            else
            {
                Console.WriteLine("\nRegistro incorrecto, Vuelva a intentarlo, recuerde no dejar campos vacios");
                PararPantalla();
                AgregarPasante();
            }
            return true;
        }

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

                if (passw2 == "0") return Salir();


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
        private static Boolean AgregarBarrio()
        {
            Console.WriteLine("\nAgregar un Barrio");
            Console.WriteLine("\nNombre: [0 --> Salir]");
            String name = Console.ReadLine();

            if (name == "0") return Salir();

            Console.WriteLine("\nDescripcion: ");
            String desc = Console.ReadLine();
            repoBarrio.Add(new Barrio
            {
                nombre = name,
                descripcion = desc
            });

            PararPantalla();
            return true;

        }

        private static void ListarBarrios()
        {
            Console.WriteLine("\nListar Barrios");
            var lista = repoBarrio.FindAll();
            MostrarLista(lista);
            PararPantalla();
        }

        private static Boolean ModificarBarrio()
        {
            Boolean modified = false;
            String nombre = null;
            String descripcion = null;

            Console.WriteLine("\nModificar Barrio");

            Console.WriteLine("\nNombre: [0 --> Salir]");
            nombre = Console.ReadLine();

            Console.WriteLine("\nDescripcion:");
            descripcion = Console.ReadLine();

            if (descripcion == "0") return Salir();

            if (nombre == "" || descripcion == "")
            {
                Console.WriteLine("\n\nLos campos no pueden estar vacios, Intente de nuevo!");
                PararPantalla();
                ModificarBarrio();
            }

            modified = repoBarrio.Update(new Barrio
            {
                nombre = nombre,
                descripcion = descripcion
            });

            if (modified)
            {
                Console.WriteLine("\nAniadido Correctamente!");
                PararPantalla();
                DibujarMenu();
            }
            else
            {
                Console.WriteLine("\nOoops! \n\tHa ocurrido un error, asegurate que el barrio exista, Intente nuevamente!");
                PararPantalla();
                ModificarPasante();
            }

            return true;

        }

        private static Boolean EliminarBarrio()
        {
            Boolean deleted = false;

            Console.WriteLine("\nEliminar un Barrio");

            while (!deleted)
            {
                Console.WriteLine("\nNombre: [0 --> Salir]");
                String name = Console.ReadLine();

                if (name == "0") return Salir();

                deleted = repoBarrio.Delete(new Barrio
                {
                    nombre = name,
                    descripcion = null
                });

                if (!deleted)
                {
                    Console.WriteLine("El barrio no se pudo eliminar, verifique que el barrio exista");
                    PararPantalla();
                    EliminarBarrio();
                } else
                {
                    Console.WriteLine("El barrio se ha eliminado!");
                    PararPantalla();
                }
            }



            return true;
        }

        #endregion

        #region VISUAL
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
        #endregion
    }
}
