using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Models
{
    public class ResumenTrabajadores
    {
        public int SumaSalarial { get; set; }
        public int MediaSalarial { get; set; }
        public List<Trabajador> Trabajadores { get; set; }
    }
}
