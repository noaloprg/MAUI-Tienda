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

        private const String NOMBRE_TABLA = "clientes";

        public static String CreateTable()
        {
            return $"CREATE TABLE IF NOT EXISTS {NOMBRE_TABLA} (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "correo VARCHAR," +
                "apellidos VARCHAR, " +
                "nombre VARCHAR," +
                "ciudad VARCHAR," +
                "comentario TEXT," +
                "vip INTEGER," +
                ");";
        }

        public static String Insert()
        {
            return $"INSERT INTO {NOMBRE_TABLA} (correo, apellidos, nombre, ciudad, comentario, vip)" +
                $" VALUES (@correo, @apellidos, @nombre, @ciudad, @comentario, @vip);" ;
        }

        public static String DeleteClieneByCorreo()
        {
            return $"DELETE FROM {NOMBRE_TABLA} WHERE correo = @correo";
        }

        public static String UpdateAllFields()
        {
            return $"UPDATE {NOMBRE_TABLA} " +
                $"SET " +
                $"correo = @correo, " +
                $"apellidos = @apellidos, " +
                $"nombre = @nombre, " +
                $"ciudad = @ciudad, " +
                $"comentrio =  @comentario, " +
                $"vip = @vip " +
                $"WHERE id = @id;";
        }
    }
}
