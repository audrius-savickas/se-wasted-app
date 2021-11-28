using Domain.Models;
using Persistence.File.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Persistence.File.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly string _pathToDatabase;

        public BaseRepository(string pathToDatabase)
        {
            _pathToDatabase = pathToDatabase;
        }

        public void Add(T entity)
        {
            List<T> all = GetAll().ToList();
            all.Add(entity);
            WriteAllToFile(all);
        }

        public void Delete(string id)
        {
            List<T> all = GetAll().ToList();
            all.Remove(all.Find(x => x.Id == id));
            WriteAllToFile(all);
        }

        public IEnumerable<T> GetAll()
        {
            string jsonAsString = System.IO.File.ReadAllText(_pathToDatabase);
            List<T> all = JsonSerializer.Deserialize<List<T>>(jsonAsString);
            return all;
        }

        public T GetById(string id)
        {
            IEnumerable<T> all = GetAll();
            T entity = all.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void Update(T entity)
        {
            List<T> all = GetAll().ToList();

            // Update
            int indexOfEntity = all.FindIndex(0, t => t.Id == entity.Id);
            all[indexOfEntity] = entity;

            WriteAllToFile(all);
        }

        private void WriteAllToFile(IEnumerable<T> all)
        {
            string jsonString = JsonSerializer.Serialize(all);
            System.IO.File.WriteAllText(_pathToDatabase, jsonString);
        }

    }
}
