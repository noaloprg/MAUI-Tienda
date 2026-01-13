using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Convertidores
{
    internal class VIPConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            /*
             * convierte el object del valor en booleano
             * si es true pone una imagen y si no otra
             */
            return (bool)value ? "vip.png" : "public.png";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /*
         * XAML
         *<Image  Source="{Binding vip, Converter={StaticResource VIPConverter}}>
         *
         *APP.XAML
         *xmlns:converters="clr-namespace:Tienda.Convertidores"
         */
    }
}
