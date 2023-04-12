using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDTOService<T> where T : IDTOEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        T Add(T entity);
        void Update(T entity);
    }
}
