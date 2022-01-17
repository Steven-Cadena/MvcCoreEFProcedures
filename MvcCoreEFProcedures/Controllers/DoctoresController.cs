using Microsoft.AspNetCore.Mvc;
using MvcCoreEFProcedures.Models;
using MvcCoreEFProcedures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFProcedures.Controllers
{
    public class DoctoresController : Controller
    {
        //resolvemos las dependencias con el repo 
        private RepositoryDoctores repo;
        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }
        public IActionResult Index() 
        {
            return View();
        }
        public IActionResult DoctoresIncrementoSalarial()
        {
            List<string> especialidades = this.repo.GetEspecialidades();
            ViewBag.Especialidad = especialidades;
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }
        [HttpPost]
        public IActionResult DoctoresIncrementoSalarial(string especialidad,int incremento) 
        {
            List<string> especialidades = this.repo.GetEspecialidades();
            ViewBag.Especialidad = especialidades;

            this.repo.IncrementarSalario(especialidad, incremento);

            List<Doctor> doctores = this.repo.FindDoctoresEspecialidad(especialidad);

            return View(doctores);
        }
    }
}
