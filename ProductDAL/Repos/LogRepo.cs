using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductDAL.Models;

namespace ProductDAL.Repos
{
	public class LogRepo : BaseRepo, IRepo<Log>
	{
		public int Add(Log entity)
		{
			Context.Log.Add(entity);
			return SaveChanges();
		}

		public int AddRange(IList<Log> entities)
		{
			Context.Log.AddRange(entities);
			return SaveChanges();
		}

        public int Save(Log entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			return SaveChanges();
		}

		public int Delete(Log entity)
		{
			Context.Entry(entity).State = EntityState.Deleted;
			return SaveChanges();
		}
		public Log GetOne(int? id)
			=> Context.Log.Find(id);

		public List<Log> GetAll()
			=> Context.Log.ToList();
	}
}
