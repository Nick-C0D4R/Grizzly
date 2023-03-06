using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IContextRepository<T> where T : IContextTable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void AddUpdate(T entity);

        void Remove(T entity);
    }
}
