using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReservationService
    {
        string MakeReservation(string foodId, string customerId);
        void CancelReservation(string foodId, string customerId);
    }
}
