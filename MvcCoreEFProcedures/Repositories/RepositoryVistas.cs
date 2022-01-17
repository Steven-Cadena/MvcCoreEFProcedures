using MvcCoreEFProcedures.Data;
using MvcCoreEFProcedures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Repositories
{
    public class RepositoryVistas
    {
        private HospitalContext context;

        public RepositoryVistas(HospitalContext context) 
        {
            this.context = context;
        }

        public List<VistaEmpleado> GetVistaEmpleados() 
        {
            var consulta = from datos in this.context.VistaEmpleados
                           select datos;
            return consulta.ToList();
        }
    }
}
