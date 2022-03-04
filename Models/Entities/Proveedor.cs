using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Models.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoDePersona TipoPersona { get; set; }
        public string Cedula { get; set; }
        public string CuentaContable { get; set; }
        public decimal Balance { get; set; }
        public bool Estado { get; set; }
        public enum TipoDePersona
        {

            Fisica,
            Juridica
        }
    }
}
