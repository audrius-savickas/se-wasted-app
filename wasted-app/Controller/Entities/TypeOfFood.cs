using System;
using System.Text.Json;
using wasted_app.Controller.Entities;

namespace console_wasted_app.Controller.Entities
{
    public class TypeOfFood : BaseEntity
    {

        public TypeOfFood(string id, string name) : base(id,name)
        {}

        public TypeOfFood(JsonElement json) : base(json)
        {}
    }
}
