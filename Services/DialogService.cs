using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Services
{
    internal class DialogService
    {

        // metodo para mostrar un dialogo de alerta
        private static async void ShowAlertDialog(string titulo, string contenido)
        {
            if (Shell.Current != null)
            {
                await Shell.Current.DisplayAlert(titulo, contenido, "volver");
            }
        }

        // Muestra un dialogo cuando la accion es correcta
        public static async void AlertDialogCorrecto(string accion, string mensaje)
        {
            // Ejemplo: Guardado exitoso
            ShowAlertDialog($"{accion} exitoso", mensaje);
        }

        // Muestra un dialogo GENERICO cuando la accion sufre un error
        public static async void AlertDialogError(string accion)
        {
            // Ejemplo: error de eliminado / eliminar...
            ShowAlertDialog($"Error de {accion}", $"Algo salió mal al intentar {accion}, vuelva a intentarlo");
        }

        // Muestra un dialogo cuando la accion es incorrecta
        public static async void AlertDialogError(string causa, string mensaje)
        {
            // 
            ShowAlertDialog($"Error. {causa}", $"{mensaje}, vuelva a intentarlo");
        }

    }
}
