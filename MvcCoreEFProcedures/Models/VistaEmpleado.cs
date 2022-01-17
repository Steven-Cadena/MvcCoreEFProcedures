using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Models
{
    [Table("EMPLEADOS_DEPARTAMENTOS")]
    public class VistaEmpleado
    {
        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }
        [Column(("APELLIDO"))]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("DEPARTAMENTO")]
        public string Departamento { get; set; }
        [Column("LOCALIDAD")]
        public string Localidad { get; set; }
    }
}

