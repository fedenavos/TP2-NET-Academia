using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.logic;
using Business.Entities;
using System.Runtime.Remoting.Messaging;

namespace UI.Consola {
    class Usuarios {
        UsuarioLogic usuarioNegocio;

        public Usuarios() {
            usuarioNegocio = new UsuarioLogic();
        }

        public void menu() {
            int opc;
            do {
                Console.WriteLine("1 - Listado general");
                Console.WriteLine("2 - Consulta");
                Console.WriteLine("3 - Agregar");
                Console.WriteLine("4 - Modificar");
                Console.WriteLine("5 - Eliminar");
                Console.WriteLine("6 - Salir");
                opc = ingresarNumero("Ingrese una opcion");

                switch (opc) {
                    case 1:
                        listadoGeneral();
                        break;
                    case 2:
                        consulta();
                        break;
                    case 3:
                        agregar();
                        break;
                    case 4:
                        modificar();
                        break;
                    case 5:
                        eliminar();
                        break;
                }

            } while (opc != 6);

        }


        /* --- Opciones del menu --- */
        private void listadoGeneral() {
            List<Usuario> usuarios = usuarioNegocio.getAll();

            foreach(Usuario usr in usuarios) {
                mostrarDatos(usr);
            }
        }
        private void consulta() {
            int id = ingresarNumero("Ingrese el ID del usuario a consultar");
            Usuario usr = usuarioNegocio.getOne(id);

            if (usr == null) {
                Console.WriteLine("No hay un usuario con el ID ingresado");
                return;
            }
            mostrarDatos(usr);
        }
        private void agregar() {
            Usuario usr = ingresarDatosUsuario();
            usr.State = BusinessEntity.States.New;

            usuarioNegocio.save(usr);
            Console.WriteLine("ID: " + usr.Id);
        }
        private void modificar() {
            int id = ingresarNumero("Ingrese el ID del usuario a modificar");
            Usuario usr = usuarioNegocio.getOne(id);

            if (usr == null) {
                Console.WriteLine("No hay un usuario con el ID ingresado");
                return;
            }

            usr = ingresarDatosUsuario();
            usr.Id = id;
            usr.State = BusinessEntity.States.Modified;

            usuarioNegocio.save(usr);
        }
        private void eliminar() {
            int id = ingresarNumero("Ingrese el ID del usuario a modificar");
            usuarioNegocio.delete(id);
        }

        /* --- Salida de datos --- */
        private void mostrarDatos(Usuario usr) {
            Console.WriteLine("Usuario: " + usr.Id);
            Console.WriteLine("\t\tNombre de Usuario: " + usr.NombreUsuario);
            Console.WriteLine("\t\tClave: " + usr.Clave);
            Console.WriteLine("\t\tHabilitado: " + usr.Habilitado);

            Console.WriteLine();
        }

        /* --- Entrada de datos --- */
        private int ingresarNumero(String mensaje) {
            int resp;
            Console.WriteLine(mensaje);
            do {
                try {
                    resp = int.Parse(Console.ReadLine());
                }
                catch (FormatException e) {
                    Console.WriteLine("Entrada no valida: " + e.Message);
                    resp = -1;
                }
            } while (resp == -1);

            return resp;
        }
        private Usuario ingresarDatosUsuario() {
            Usuario usr = new Usuario();
            Console.WriteLine("Ingrese el nombre de usuario");
            usr.NombreUsuario = Console.ReadLine();
            Console.WriteLine("Ingrese la clave");
            usr.Clave = Console.ReadLine();
            Console.WriteLine("Ingrese la habilitacion del usuario (1 si, cualquier otra entrada no)");
            usr.Habilitado = (Console.ReadLine() == "1");

            return usr;
        }
    }
}
