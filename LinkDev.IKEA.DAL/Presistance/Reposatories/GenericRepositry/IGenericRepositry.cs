using LinkDev.IKEA.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Presistance.Reposatories.GenericRepositry
{
	public interface IGenericRepositry<T> where T : ModelBase
	{
		IEnumerable<T> GetAll(bool AsNoTracking = true);
		IQueryable<T> GetAllIQueryable();
		T? Get(int id);
		void Add(T department);
		void Update(T department);
		void Delete(T department);

	}
}
