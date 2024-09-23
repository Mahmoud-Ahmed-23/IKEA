
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Presistance.Data;
using LinkDev.IKEA.DAL.Presistance.Reposatories.GenericRepositry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.Departments
{
    public class DepartmentRepositry : GenericRepositry<Department>, IDepartmentRepositry
    {
        public DepartmentRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        
    }
}
