using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Models.Entities
{
    public class Solicitud
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public string CuentaContableProveedor { get; set; }
        public string CuentaContableBanco { get; set; }

        public enum EstadoSolicitud
        {
            Pendiente,
            Anulada,
            Generado
        }
    }
}
