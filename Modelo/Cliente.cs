using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Modelo
{
    class Cliente
    {
        public Cliente(string nombre, string apellidos, string correo, string ciudad, string comentario, bool vip)
        {
            this.correo = correo;
            this.apellidos = apellidos;
            this.nombre = nombre;
            this.ciudad = ciudad;
            this.comentario = comentario;
            this.vip = vip;
            this.nombreCompleto = $"{apellidos} {nombre}";
        }

        public Cliente()
        {

        }

        public string correo {  get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string ciudad {  get; set; }
        public string comentario { get; set; }
        public bool vip {  get; set; }

        public string nombreCompleto { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Cliente cliente &&
                   correo == cliente.correo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(correo);
        }

        public override string ToString()
        {
            return $"{nombre} - {correo} - {vip}";
        }
    }
}
