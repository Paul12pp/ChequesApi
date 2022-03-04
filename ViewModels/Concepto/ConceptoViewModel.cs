using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.ViewModels.Concepto
{
    public class ConceptoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
