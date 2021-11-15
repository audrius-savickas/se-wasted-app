using Domain.Entities;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(string id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
