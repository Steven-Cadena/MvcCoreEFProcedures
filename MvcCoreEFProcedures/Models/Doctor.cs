using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Models
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Key]
        [Column("HOSPITAL_COD")]
        public int IdHospital { get; set; }
        [Column("DOCTOR_NO")]
        public int IdDoctor { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("ESPECIALIDAD")]
        public string Especialidad { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
    }
}
