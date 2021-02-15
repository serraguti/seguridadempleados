using Microsoft.AspNetCore.Mvc;
using SeguridadEmpleados.Filters;
using SeguridadEmpleados.Models;
using SeguridadEmpleados.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        [AuthorizeEmpleados]
        public IActionResult Perfil()
        {
            String dato =
                   HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int empno = int.Parse(dato);
            Empleado emp = this.repo.BuscarEmpleado(empno);
            return View(emp);
        }

        [AuthorizeEmpleados]
        public IActionResult EmpleadosSubordinados()
        {
            String dato =
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int empno = int.Parse(dato);
            List<Empleado> empleados = this.repo.GetSubordinados(empno);
            return View(empleados);
        }
    }
}
