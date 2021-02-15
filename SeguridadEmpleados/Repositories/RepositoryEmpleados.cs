using SeguridadEmpleados.Data;
using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SeguridadEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public Empleado ExisteEmpleado(String apellido, int idempleado)
        {
            return this.context.Empleados
                .SingleOrDefault(x => x.Apellido == apellido
                && x.IdEmpleado == idempleado);
        }

        public List<Empleado> GetSubordinados(int idempleado) {
            var consulta = from datos in context.Empleados
                           where datos.Director == idempleado
                           select datos;
            return consulta.ToList();
        }

        public Empleado BuscarEmpleado(int idempleado)
        {
            return this.context.Empleados
                .SingleOrDefault(x => x.IdEmpleado == idempleado);
        }
    }
}
