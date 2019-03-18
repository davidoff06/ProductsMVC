using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductDAL.Models;

namespace ProductDAL.Repos
{
	public class ProductRepo : BaseRepo, IRepo<Product>
	{
		public int Add(Product entity)
		{
			Context.Product.Add(entity);
			return SaveChanges();
		}

		public int AddRange(IList<Product> entities)
		{
			Context.Product.AddRange(entities);
			return SaveChanges();
		}

        public int Save(Product entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			return SaveChanges();
		}

		public int Delete(Product entity)
		{
			Context.Entry(entity).State = EntityState.Deleted;
			return SaveChanges();
		}
		public Product GetOne(int? id)
			=> Context.Product.Find(id);

		public List<Product> GetAll()
			=> Context.Product.ToList();
	}
}
