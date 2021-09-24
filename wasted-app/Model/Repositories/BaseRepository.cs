using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using console_wasted_app.Controller.Interfaces;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Model.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly string _pathToDatabase;

        public BaseRepository(String pathToDatabase)
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
            List<T> all = new List<T>();
            string jsonAsString = System.IO.File.ReadAllText(_pathToDatabase);
                 
            using (JsonDocument document = JsonDocument.Parse(jsonAsString))
            {
                JsonElement root = document.RootElement;
                JsonElement.ArrayEnumerator iterator = root.EnumerateArray();

                while (iterator.MoveNext())
                {
                    JsonElement json = iterator.Current;
                    T element = (T)Activator.CreateInstance(typeof(T), json);
                    all.Add(element);
                }
            }

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
