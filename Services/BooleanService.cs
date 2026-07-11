using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Services
{
    // servicio para operar sobre booleanos
    internal class BooleanService
    {

        // convierte un booleano en un Integer para que la BD Sqlite pueda almacenarlo
        public static int ToInt(bool valor)
        {
            return valor ? 1 : 0;
        }

        // convierte un numero (0 - 1) en booleano para trabajar mas facilmente
        public static bool ToBool(int valor)
        {
            // 0 = false, 1 = true
            return valor > 0 ? true : false;
        }
    }
}
