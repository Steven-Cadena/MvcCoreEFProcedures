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
//create procedure SP_ALLENFERMOS 
//as
//	select * from enfermo
//go

//create procedure SP_FINDENFERMO (@INSCRIPCION INT)
//as
//    select* from enfermo

//    where INSCRIPCION = @INSCRIPCION
//go

//create procedure SP_DELETEENFERMO(@INSCRIPCION INT)
//as
//    delete from ENFERMO

//    where INSCRIPCION = @INSCRIPCION
//go
#endregion
namespace MvcCoreEFProcedures.Repositories
{
    public class RepositoryEnfermos
    {
        private EnfermosContext context;
        public RepositoryEnfermos(EnfermosContext context) 
        {
            this.context = context;
        }

        /******IMPORTANTE HECHO CON ADO***************/
        public List<Enfermo> GetEnfermos() 
        {
            //PARA LLAMAR A PROCEDIMIENTOS SELECT DEBEMOS EXTRAER LA CONEXION DE NUESTRO CONTEXT
            //SE UTILIZAN OBJETOS A LA ANTIGUA DE ADO (EF)
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand()) {
                string sql = "SP_ALLENFERMOS";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                List<Enfermo> enfermos = new List<Enfermo>();
                while (reader.Read()) 
                {
                    Enfermo enfermo = new Enfermo();
                    enfermo.Inscripcion = int.Parse(reader["INSCRIPCION"].ToString());
                    enfermo.Apellido = reader["APELLIDO"].ToString();
                    enfermo.Direccion = reader["DIRECCION"].ToString();
                    enfermo.FechaNac = DateTime.Parse(reader["FECHA_NAC"].ToString());
                    enfermo.Genero = reader["S"].ToString();
                    enfermo.NSS = reader["NSS"].ToString();
                    enfermos.Add(enfermo);
                }
                reader.Close();
                com.Connection.Close();
                return enfermos;
            }
        }
        /******IMPORTANTE HECHO CON LINQ***************/
        public Enfermo FindEnfermo(int inscripcion) 
        {
            //PARA LLAMAR A LOS PROCEDIMIENTOS CON PARAMETROS SE 
            //REALIZA DE LA SIGUIENTE FORMA 
            // SP_PROCEDURE @PARAM1,@PARAM2 ...
            string sql = "SP_FINDENFERMO @INSCRIPCION";
            //SE UTILIZAN PARAMETROS DE LA CLASE SqlParameter
            //PERO DEL NAMESPACE Microsoft.Data.SqlClient
            SqlParameter paminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);
            //COMO ES UN PROCEDIMIENTO DE CONSULTA DE SELECCION 
            //UTILIZAMOS EL METODO FromSqlRaw(sql, Param1,Param2)

            //CUANDO UTILIZAMOS METODOS DE LINQ EN PROCEDIMIENTOS 
            //Enfermo ENTITY FRAMEWORK, NO PODEMOS EXTRAER LAS ENTIDADES
            //Y APLICAR LOS METODOS A LA VEZ
            //DEBEMOS CONVERTIRLO A COLECCION LA CONSULTA

            var consulta = this.context.Enfermos.FromSqlRaw(sql, paminscripcion);
            Enfermo enfermo = consulta.AsEnumerable().FirstOrDefault();
            return enfermo;
        }
        /******IMPORTANTE HECHO CON LINQ***************/
        public void DeleteEnfermo(int inscripcion) 
        {
            string sql = "SP_DELETEENFERMO @INSCRIPCION";
            SqlParameter paminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);
            //para ejecutar la consulta 
            this.context.Database.ExecuteSqlRaw(sql, paminscripcion);
        }
    }
}
