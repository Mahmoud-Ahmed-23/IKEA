
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.Departments
{
    public class DepartmentRepositry : IDepartmentRepositry
    {
        private readonly ApplicationDbContext _dbcontext;

        public DepartmentRepositry(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Department? Get(int id)
        {
            return _dbcontext.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _dbcontext.Departments.ToList();

            return _dbcontext.Departments.AsNoTracking().ToList();

        }

        public int Add(Department department)
        {
            _dbcontext.Departments.Add(department);
            return _dbcontext.SaveChanges();
        }

        public int Update(Department department)
        {
            _dbcontext.Departments.Update(department);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbcontext.Departments.Remove(department);
            return _dbcontext.SaveChanges();
        }

    }
}
