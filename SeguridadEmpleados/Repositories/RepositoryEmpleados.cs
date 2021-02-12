using SeguridadEmpleados.Data;
using SeguridadEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
