using Domain.Models;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T GetById(string id);
        IEnumerable<T> GetAll();
        void Add(T model);
        void Update(T model);
        void Delete(string id);
    }
}
