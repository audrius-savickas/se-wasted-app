using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using console_wasted_app.Controller.Interfaces;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Model.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly FileStream database;

        public BaseRepository(String pathToDatabase)
        {
            this.database = new FileStream(pathToDatabase, FileMode.Open, FileAccess.ReadWrite);
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            List<T> all = new List<T>();
            using var sr = new StreamReader(this.database, System.Text.Encoding.UTF8);

            string jsonAsString = sr.ReadToEnd();

            using (JsonDocument document = JsonDocument.Parse(jsonAsString))
            {
                JsonElement root = document.RootElement;
                JsonElement.ArrayEnumerator iterator = root.EnumerateArray();

                while (iterator.MoveNext())
                {
                    JsonElement json = iterator.Current;
                    T element = new T().FromJson(json);
                    all.Add(element);
                }
            }

            return all;
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
