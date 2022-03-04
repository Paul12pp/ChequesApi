using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ChequesApi.Models.Entities.Proveedor;

namespace ChequesApi.ViewModels.Proveedor
{
    public class ProveedorViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoDePersona TipoPersona { get; set; }
        public string Cedula { get; set; }
        public string CuentaContable { get; set; }
        public decimal Balance { get; set; }
        public bool Estado { get; set; }
    }
}
