using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Services
{
    // Servicio para gestionar los controladores de tipo Entry
    internal class EntryService
    {
        // Limpia los campos de entrada
        public static void LimpiarContenidoEntries(List<Entry> listaCamposEntrada)
        {
            foreach (var entry in listaCamposEntrada)
            {
                entry.Text = null;
            }
        }

        // Comprueba que todos los entries de una lista tengan contneido escrito
        public static bool ComprobarContenidoEntries(List<Entry> listaEntradas)
        {
            foreach (var et in listaEntradas)
            {
                if (String.IsNullOrEmpty(et.Text)) return false;
            }

            return true;
        }

        // Metodo para normalizar la entrada de datos simples
        // siempre inicial mayuscula y resto en minusculas y sin espacios
        public static string NormalizarEntradaDatos(string cadena)
        {
            string cadenaSinEspacios = cadena.Trim();
            string inicial = cadenaSinEspacios.Substring(0, 1).ToUpper() ;
            string resto = cadenaSinEspacios.Substring(1).ToLower();
            return inicial + resto;
        }
    }
}
