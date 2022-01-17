using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Models
{
    [Table("VISTA_TRABAJADORES")]
    public class Trabajador
    {
        [Key]
        [Column("IDTRABAJADOR")]
        public int IdTrabajador { get; set; }
        [Column("APELLIDO")]
        public String Apellido { get; set; }
        [Column("Salario")]
        public int Salario { get; set; }
        [Column("OFICIO")]
        public String Oficio { get; set; }
    }
}
