using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.DAO
{
    // Clase que hace de servicio para realizar operaciones sobre la base de datos
    internal class DAOService
    {
        private const String NOMBRE_BD = "sprayAndSole.db";
        private static String rutaBD = $"Data source= {NOMBRE_BD}";
        private static String rutaConexion = Path.Combine(FileSystem.AppDataDirectory, rutaBD);

        // metodo privado que crea una conexion con la BD
        private static SqliteConnection GetConexion()
        {
            return new SqliteConnection(rutaConexion);
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
    }
}
