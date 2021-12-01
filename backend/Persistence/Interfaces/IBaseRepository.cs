using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T GetById(string id);
        IQueryable<T> GetAll();
        string Insert(T model);
        void Update(T model);
        void Delete(string id);
    }
}
