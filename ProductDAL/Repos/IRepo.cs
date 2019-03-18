using System.Collections.Generic;
using System.Threading.Tasks;
using ProductDAL;

namespace ProductDAL.Repos
{
	public interface IRepo<T>
	{
		int Add(T entity);
		int AddRange(IList<T> entities);
		int Save(T entity);
		int Delete(T entity);
		T GetOne(int? id);
		List<T> GetAll();

        //Task<int> AddAsync(T entity);
		//Task<int> AddRangeAsync(IList<T> entities);
		//Task<int> SaveAsync(T entity);
		//Task<int> DeleteAsync(int id, byte[] timeStamp);
		//Task<int> DeleteAsync(T entity);
		//Task<T> GetOneAsync(int? id);
		//Task<List<T>> GetAllAsync();
		//List<T> ExecuteQuery(string sql);
		//Task<List<T>> ExecuteQueryAsync(string sql);
		//List<T> ExecuteQuery(string sql,object[] sqlParametersObjects );
		//Task<List<T>> ExecuteQueryAsync(string sql, object[] sqlParametersObjects);
	}
}
