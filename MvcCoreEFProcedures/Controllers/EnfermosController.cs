using Microsoft.AspNetCore.Mvc;
using MvcCoreEFProcedures.Models;
using MvcCoreEFProcedures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Controllers
{

    public class EnfermosController : Controller
    {
        //resolvemos las dependencias con el repo 
        private RepositoryEnfermos repo;
        public EnfermosController(RepositoryEnfermos repo) 
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }
        public IActionResult Details(int inscripcion) 
        {
            Enfermo enfermo = this.repo.FindEnfermo(inscripcion);
            return View(enfermo);
        }
        public IActionResult Delete(int inscripcion) 
        {
            this.repo.DeleteEnfermo(inscripcion);
            return RedirectToAction("Index");
        }
    }
}
