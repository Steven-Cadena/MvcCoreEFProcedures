using Microsoft.AspNetCore.Mvc;
using MvcCoreEFProcedures.Models;
using MvcCoreEFProcedures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Controllers
{

    public class TrabajadoresController : Controller
    {
        //dependencias
        private RepositoryTrabajadores repo;
        public TrabajadoresController(RepositoryTrabajadores repo)
        {
            this.repo = repo;
        }



        public IActionResult Index()
        {
            List<string> oficios = this.repo.GetOficios();
            ViewData["OFICIOS"] = oficios;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string oficio) 
        {
            List<string> oficios = this.repo.GetOficios();
            ViewData["OFICIOS"] = oficios;
            ResumenTrabajadores datos = this.repo.GetResumenTrabajadores(oficio);
            return View(datos);
        }
    }
}
