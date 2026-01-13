using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Modelo
{
    class ClienteRepository
    {
        private static string rutaDir =
            Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Ficheros");

        private static string nombreFichero = "datosClientes.txt";
        private static string rutaCompleta = Path.Combine(rutaDir, nombreFichero);

        private static List<Cliente> listaClientes = new List<Cliente>();

         public static bool SaveAll(List<Cliente> lista)
        {
            bool isGuardado;
            if (lista != null)
            {
                listaClientes = lista;
                GuardarEnFichero(lista);
                isGuardado = true;
            }
            else isGuardado = false;
            return isGuardado;
        }

        //método privado que guarda en el fichero una lista, lista que se pasa a otro método para no guardar sin antes verificar
        private static void GuardarEnFichero(List<Cliente> lista)
        {
            try
            {
                using (var sw = new StreamWriter(rutaCompleta))
                {
                    foreach (var cl in lista)
                    {
                        if (cl != null)
                        {
                            sw.WriteLine($"{cl.nombre}#{cl.apellidos}#{cl.ciudad}#{cl.correo}#{cl.comentario}#{cl.vip}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer clientes: {ex.Message}");

            }
        }

        //lee el fichero y guarda el contenido en una lista estatica que luego devuelve
        public static List<Cliente> GetClientesTxt()
        {
            string[] datosClientes;
            string linea;
            Cliente cliente;
            listaClientes = new List<Cliente>();

            if (!File.Exists(rutaCompleta)) return listaClientes;

            try
            {
                using (var sr = new StreamReader(rutaCompleta))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linea))
                        {
                            datosClientes = linea.Split("#");
                            cliente = CrearClienteTxt(datosClientes);

                            if (cliente != null)
                            {
                                listaClientes.Add(cliente);
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer clientes: {ex.Message}");
            }
            return listaClientes;
        }

        //crea el cliente segun un array de datos (datos de clientes)
        private static Cliente CrearClienteTxt(string[] datos)
        {
            string nomb = datos[0];
            string apell = datos[1];
            string ciu = datos[2];
            string email = datos[3];
            string coment = datos[4];
            bool vip = Boolean.Parse(datos[5]);


            return new Cliente(nomb, apell, email, ciu, coment, vip);
        }
    }
}
