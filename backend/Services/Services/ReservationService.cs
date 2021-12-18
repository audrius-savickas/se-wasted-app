using Domain.Models;
using Persistence.Interfaces;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _cutomerRepository;

        public ReservationService
        (
            IFoodRepository foodRepository,
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository
        )
        {
            _foodRepository = foodRepository;
            _reservationRepository = reservationRepository;
            _cutomerRepository = customerRepository;
        }
        public string MakeReservation(string foodId, string customerId)
        {
            Food food = ValidateFoodExistense(foodId);
            Customer customer = ValidateCustomerExistense(customerId);

            if (food.Reservation != null)
            {
                throw new AuthorizationException("Food item is already reserved.");
            }

            Reservation reservation = new Reservation(null, false, food.Id, food.IdRestaurant, customer.Id, food.CalculateCurrentPrice());
            return _reservationRepository.Insert(reservation);
        }

        public void CancelReservation(string foodId, string customerId)
        {
            Food food = ValidateFoodExistense(foodId);
            ValidateCustomerExistense(customerId);
            Reservation reservation = food.Reservation;

            _ = reservation ?? throw new EntityNotFoundException("No reservation was found.");
            
            if (!string.Equals(reservation.CustomerId, customerId, StringComparison.OrdinalIgnoreCase))
            {
                throw new AuthorizationException("Food is reserved by a different customer.");
            }

            reservation.IsCancelled = true;
            _reservationRepository.Update(reservation);
        }

        private Food ValidateFoodExistense(string foodId)
        {
            Food food = _foodRepository.GetById(foodId);
            _ = food ?? throw new EntityNotFoundException("Food with given id was not found.");
            return food;
        }

        private Customer ValidateCustomerExistense(string customerId)
        {
            Customer customer = _cutomerRepository.GetById(customerId);
            _ = customer ?? throw new EntityNotFoundException("Customer with given id was not found.");
            return customer;
        }
    }
}
