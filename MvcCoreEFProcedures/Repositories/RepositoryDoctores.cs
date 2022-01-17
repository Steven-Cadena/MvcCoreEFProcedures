using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFProcedures.Data;
using MvcCoreEFProcedures.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;


#region PROCEDIMIENTOS ALMACENADOS
//create procedure SP_ESPECIALIDADES
//as
//	select distinct(especialidad) from doctor
//go

//create procedure SP_GETALLDOCTORES
//as
//	select * from DOCTOR
//go

//create procedure SP_GETDOCTORESESPECIALIDAD(@ESPECIALIDAD NVARCHAR(50))
//as
//    select* from doctor
//    where ESPECIALIDAD = @ESPECIALIDAD
//go

//create procedure SP_INCREMENTOSALARIODOCTOR(@ESPECIALIDAD NVARCHAR(50), @INCREMENTO INT)
//as
//    update doctor set SALARIO = SALARIO + @INCREMENTO 
//	where ESPECIALIDAD = @ESPECIALIDAD
//go
#endregion
namespace MvcCoreEFProcedures.Repositories
{
    public class RepositoryDoctores
    {

        private EnfermosContext context;
        public RepositoryDoctores(EnfermosContext context)
        {
            this.context = context;
        }
        /*REALIZADO CON ADO EF*/
        public List<string> GetEspecialidades() 
        {
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ESPECIALIDADES";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                List<string> especialidades = new List<string>();
                while (reader.Read())
                {
                    string especialidad = reader["ESPECIALIDAD"].ToString();
                    especialidades.Add(especialidad);
                }
                reader.Close();
                com.Connection.Close();
                return especialidades;
            }
        }

        public List<Doctor> GetDoctores() 
        {
            string sql = "SP_GETALLDOCTORES";
            var consulta = this.context.Doctores.FromSqlRaw(sql);
            List<Doctor> doctores = consulta.AsEnumerable().ToList();
            return doctores;
        }
        public List<Doctor> FindDoctoresEspecialidad(string especialidad) 
        {
            string sql = "SP_GETDOCTORESESPECIALIDAD @ESPECIALIDAD";
            SqlParameter pamEsp = new SqlParameter("@ESPECIALIDAD", especialidad);
            var consulta = this.context.Doctores.FromSqlRaw(sql, pamEsp);
            List<Doctor> doctores = consulta.AsEnumerable().ToList();
            if (doctores.Count() == 0) 
            {
                return null;
            }
            else 
            {
                return doctores;
            }
        }

        public void IncrementarSalario(string especialidad, int incremento) 
        {
            string sql = "SP_INCREMENTOSALARIODOCTOR @ESPECIALIDAD,@INCREMENTO";
            SqlParameter pamIn = new SqlParameter("@INCREMENTO", incremento);
            SqlParameter pamEsp = new SqlParameter("@ESPECIALIDAD", especialidad);
            this.context.Database.ExecuteSqlRaw(sql, pamIn, pamEsp);
         }
    }
}
