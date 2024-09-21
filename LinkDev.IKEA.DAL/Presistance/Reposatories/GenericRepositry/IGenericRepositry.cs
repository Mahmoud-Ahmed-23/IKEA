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
		T? Get(int id);
		int Add(T department);
		int Update(T department);
		int Delete(T department);

	}
}
