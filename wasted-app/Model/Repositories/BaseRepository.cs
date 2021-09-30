using console_wasted_app.Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Model.Repositories
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
            var all = GetAll().ToList();
            all.Add(entity);
            WriteAllToFile(all);
        }

        public void Delete(string id)
        {
            var all = GetAll().ToList();
            all.Remove(all.Find(x => x.Id == id));
            WriteAllToFile(all);
        }

        public IEnumerable<T> GetAll()
        {
            var all = new List<T>();
            var jsonAsString = System.IO.File.ReadAllText(_pathToDatabase);

            using (var document = JsonDocument.Parse(jsonAsString))
            {
                var root = document.RootElement;
                var iterator = root.EnumerateArray();

                while (iterator.MoveNext())
                {
                    var json = iterator.Current;
                    var element = (T)Activator.CreateInstance(typeof(T), json);
                    all.Add(element);
                }
            }

            return all;
        }

        public T GetById(string id)
        {
            var all = GetAll();
            var entity = all.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void Update(T entity)
        {
            var all = GetAll().ToList();

            // Update
            var indexOfEntity = all.FindIndex(0, t => t.Id == entity.Id);
            all[indexOfEntity] = entity;

            WriteAllToFile(all);
        }

        private void WriteAllToFile(IEnumerable<T> all)
        {
            var jsonString = JsonSerializer.Serialize(all);
            System.IO.File.WriteAllText(_pathToDatabase, jsonString);
        }

    }
}
