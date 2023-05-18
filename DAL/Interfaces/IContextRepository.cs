using DAL.Tables;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IContextRepository<T> where T : ContextTable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void AddUpdate(T entity);

        void Remove(T entity);
    }
}
