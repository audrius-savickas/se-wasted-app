using System.Collections.Generic;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        T getById(string Id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
