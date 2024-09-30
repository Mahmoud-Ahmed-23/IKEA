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
	public class GenericRepositry<T> : IGenericRepositry<T> where T : ModelBase
	{
		private readonly ApplicationDbContext _dbcontext;

		public GenericRepositry(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbcontext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
		{
			if (AsNoTracking)
				return await _dbcontext.Set<T>().ToListAsync();

			return await _dbcontext.Set<T>().AsNoTracking().ToListAsync();

		}
		public IQueryable<T> GetAllIQueryable()
		{
			return _dbcontext.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await _dbcontext.Set<T>().AddAsync(entity);
		}


		public void Update(T entity)
		{
			_dbcontext.Set<T>().Update(entity);
		}


		public void Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbcontext.Set<T>().Update(entity);
		}


	}
}
