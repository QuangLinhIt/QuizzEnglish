using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Common.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task<IEnumerable<T>> GetAsync(int pageIndex, int pageSize);
        Task<T?> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
