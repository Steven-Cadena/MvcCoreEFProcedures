using Microsoft.Data.SqlClient;
using MvcCoreEFProcedures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFProcedures.Models;


#region PROCEDIMIENTOS Y VISTAS SQL SERVER 
//create view VISTA_TRABAJADORES
//AS
//select ISNULL(emp_no,0) as IdTrabajador
//,APELLIDO,SALARIO,OFICIO
//FROM EMP
//UNION
//select doctor_no, apellido, salario, especialidad
//from doctor 
//UNION
//select empleado_no, apellido, salario, funcion
//from plantilla
//go 
//select * from VISTA_TRABAJADORES

//--PROCEDIEMIENTO CON PARAMETROS DE SALIDA
//create procedure SP_TRABAJADORES_OFICIO
//(@OFICIO NVARCHAR(50),@MEDIA INT OUT, @SUMA INT OUT)
//AS
//	SELECT * FROM VISTA_TRABAJADORES
//	WHERE OFICIO = @OFICIO
//	SELECT @MEDIA = AVG(SALARIO)
//	,@SUMA = SUM(SALARIO) FROM VISTA_TRABAJADORES
//	WHERE OFICIO =@OFICIO
//GO
#endregion
namespace MvcCoreEFProcedures.Repositories
{
    
    public class RepositoryTrabajadores
    {
        private HospitalContext context;
        public RepositoryTrabajadores(HospitalContext context)
        {
            this.context = context;
        }

        public List<String> GetOficios() 
        {
            var consulta = (from datos in this.context.Trabajadores
                           select datos.Oficio).Distinct();
            return consulta.ToList();
        }

        public ResumenTrabajadores GetResumenTrabajadores(string oficio) 
        {
            //LOS PARAMETROS DE SALIDA SE DEBEN INDICAR EN LA LLAMADA CON LA PALABRA OUT
            string sql = "SP_TRABAJADORES_OFICIO @OFICIO,@MEDIA out,@SUMA out";
            SqlParameter paramofi = new SqlParameter("@OFICIO", oficio);
            //TODOS LOS PARAMETROS DEBEN TENER UN VALOR POR DEFECTO EN LA LLAMADA 
            //importante para los parametros de salida **************************************************************
            SqlParameter parammedia = new SqlParameter("@MEDIA", -1);
            parammedia.Direction = ParameterDirection.Output;
            SqlParameter paramsuma = new SqlParameter("@SUMA", -1);
            paramsuma.Direction = ParameterDirection.Output;
            //EJECUTAMOS EL PROCEDIMIENTO
            //importante el orden de los parametros
            var consulta = this.context.Trabajadores.FromSqlRaw(sql, paramofi, parammedia, paramsuma);
            ResumenTrabajadores resumen = new ResumenTrabajadores();
            resumen.Trabajadores = consulta.ToList();
            resumen.SumaSalarial = int.Parse(paramsuma.Value.ToString());
            resumen.MediaSalarial = int.Parse(parammedia.Value.ToString());
            return resumen;
        }
    }
}
