using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Presistance.Data;
using LinkDev.IKEA.DAL.Presistance.Reposatories.GenericRepositry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.Employees
{
    public class EmployeeRepositry : GenericRepositry<Employee>, IEmployeeRepositry
    {

        public EmployeeRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
