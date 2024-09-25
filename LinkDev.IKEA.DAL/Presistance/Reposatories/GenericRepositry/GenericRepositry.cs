using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.GenericRepositry
{
	public class GenericRepositry<T> where T : ModelBase
	{
		private readonly ApplicationDbContext _dbcontext;

		public GenericRepositry(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public T? Get(int id)
		{
			return _dbcontext.Set<T>().Find(id);
		}

		public IEnumerable<T> GetAll(bool AsNoTracking = true)
		{
			if (AsNoTracking)
				return _dbcontext.Set<T>().ToList();

			return _dbcontext.Set<T>().AsNoTracking().ToList();

		}
		public IQueryable<T> GetAllIQueryable()
		{
			return _dbcontext.Set<T>();
		}

		public int Add(T entity)
		{
			_dbcontext.Set<T>().Add(entity);
			return _dbcontext.SaveChanges();
		}


		public int Update(T entity)
		{
			_dbcontext.Set<T>().Update(entity);
			return _dbcontext.SaveChanges();
		}


		public int Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbcontext.Set<T>().Update(entity);
			return _dbcontext.SaveChanges();
		}
	}
}
