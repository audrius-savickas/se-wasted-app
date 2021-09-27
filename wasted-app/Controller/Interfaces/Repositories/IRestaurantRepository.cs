using System;
using System.Collections.Generic;
using console_wasted_app.Controller.Entities;
using console_wasted_app.Controller.Interfaces;

namespace console_wasted_app.Model.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        public IEnumerable<Restaurant> GetRestaurantsNear(Coords coords);
        public Restaurant GetByMail(Mail mail);
    }
}
