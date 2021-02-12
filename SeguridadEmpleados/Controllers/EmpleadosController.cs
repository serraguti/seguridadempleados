using Microsoft.AspNetCore.Mvc;
using SeguridadEmpleados.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        [AuthorizeEmpleados]
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
