using wasted_app.Controller.Entities;

namespace backend.Controller.Entities
{
    public class TypeOfFood : BaseEntity
    {
        public TypeOfFood() : base() { }

        public TypeOfFood(string id, string name) : base(id, name)
        { }
    }
}
