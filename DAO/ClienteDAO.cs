using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.DAO
{
    // Clase donde se definen las QUERYs SQLITE de la tabla de Clientes
    internal class ClienteDAO
    {

        private const string NOMBRE_TABLA = "clientes";

        public static string CreateTable()
        {
            return $"CREATE TABLE IF NOT EXISTS {NOMBRE_TABLA} (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "correo VARCHAR," +
                "apellidos VARCHAR, " +
                "nombre VARCHAR," +
                "ciudad VARCHAR," +
                "comentario TEXT," +
                "vip INTEGER" +
                ");";
        }

        public static string InsertNewCliente()
        {
            return $"INSERT INTO {NOMBRE_TABLA} (correo, apellidos, nombre, ciudad, comentario, vip)" +
                $" VALUES (@correo, @apellidos, @nombre, @ciudad, @comentario, @vip);";
        }

        // elimina a un cliente según el correo
        public static string DeleteClieneByCorreo()
        {
            return $"DELETE FROM {NOMBRE_TABLA} WHERE correo = @correo";
        }


        // actualizado completo 
        public static string UpdateAllFields()
        {
            return $"UPDATE {NOMBRE_TABLA} " +
                $"SET " +
                $"correo = @correo, " +
                $"apellidos = @apellidos, " +
                $"nombre = @nombre, " +
                $"ciudad = @ciudad, " +
                $"comentario =  @comentario, " +
                $"vip = @vip " +
                $"WHERE id = @id;";
        }

        public static string GetAll()
        {
            return $"SELECT * FROM {NOMBRE_TABLA}";
        }
    }
}
