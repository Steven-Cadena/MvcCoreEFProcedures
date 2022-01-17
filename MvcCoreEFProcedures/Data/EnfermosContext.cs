using Microsoft.EntityFrameworkCore;
using MvcCoreEFProcedures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region vistas sql server
//create view EMPLEADOS_DEPARTAMENTO
//AS
//select ISNULL(EMP.EMP_NO, 0)AS EMP_NO, EMP.APELLIDO, EMP.OFICIO 
//, DEPT.DNOMBRE as DEPARTAMENTO
//, DEPT.LOC as LOCALIDAD
//from emp 
//inner join DEPT
//on EMP.DEPT_NO = DEPT.DEPT_NO
#endregion
namespace MvcCoreEFProcedures.Data
{
    public class EnfermosContext:DbContext
    {
        public EnfermosContext(DbContextOptions<EnfermosContext> options): base(options) { }

        public DbSet<Enfermo> Enfermos { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
    }
}
