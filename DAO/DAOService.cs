using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Modelo;
using Tienda.Services;

namespace Tienda.DAO
{
    // Clase que hace de servicio para realizar operaciones sobre la base de datos
    internal class DAOService
    {
        private const String NOMBRE_BD = "sprayAndSole.db";
        private static String rutaConexion = Path.Combine(FileSystem.AppDataDirectory, NOMBRE_BD);
        private static String rutaBD = $"Data source={rutaConexion}";

        // metodo privado que crea una conexion con la BD
        private static SqliteConnection GetConexion()
        {
            return new SqliteConnection(rutaBD);
        }

        // Crea las tablas iniciales
        public static async Task<bool> CrearTablas()
        {
            try
            {
                using (var conexion = GetConexion())
                {
                    await conexion.OpenAsync();

                    using (var command = new SqliteCommand(ClienteDAO.CreateTable(), conexion))
                    {
                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Obtiene todos los clientes
        public static async Task<List<Cliente>> GetAllClientes()
        {
            List<Cliente> clientesBD = new List<Cliente>();
            Cliente cl;

            try
            {
                using (var conexion = GetConexion())
                {
                    await conexion.OpenAsync();

                    using (var command = new SqliteCommand(ClienteDAO.GetAll(), conexion))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // cliente que lee en cada iteracion
                                cl = new Cliente();

                                cl.id = reader.GetInt32(0);
                                cl.correo = reader.GetString(1);
                                cl.apellidos = reader.GetString(2);
                                cl.nombre = reader.GetString(3);
                                cl.ciudad = reader.GetString(4);
                                cl.comentario = reader.GetString(5);
                                cl.vip = BooleanService.ToBool(reader.GetInt32(6));

                                clientesBD.Add(cl);
                            }
                        }

                        return clientesBD;
                    }
                }
            }
            catch (Exception e)
            {
                return clientesBD;
            }
        }

        // Modifcia el cliente con los valores intrroducidos
        // segun el booleano se crea o modifica (ya que es una modificacion completa  y no parcial)
        public static async Task<bool> ModificarInsertarCliente(Cliente cl, bool isInsert)
        {

            try
            {
                using (var conexion = GetConexion())
                {
                    await conexion.OpenAsync();

                    // segun el booleano se selecciona una query u otra
                    string cadenaSql = isInsert ? ClienteDAO.InsertNewCliente() : ClienteDAO.UpdateAllFields();

                    using (var command = new SqliteCommand(cadenaSql, conexion))
                    {
                        command.Parameters.AddWithValue("@correo", cl.correo);
                        command.Parameters.AddWithValue("@apellidos", cl.apellidos);
                        command.Parameters.AddWithValue("@nombre", cl.nombre);
                        command.Parameters.AddWithValue("@ciudad", cl.ciudad);
                        command.Parameters.AddWithValue("@comentario", cl.comentario);
                        command.Parameters.AddWithValue("@vip", BooleanService.ToInt(cl.vip));
                       
                        if (!isInsert) command.Parameters.AddWithValue("@id", cl.id);

                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
