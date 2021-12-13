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
        private readonly IRestaurantRepository _restaurantRepository;

        public ReservationService
        (
            IFoodRepository foodRepository,
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            IRestaurantRepository restaurantRepository
        )
        {
            _foodRepository = foodRepository;
            _reservationRepository = reservationRepository;
            _cutomerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
        }
        public string MakeReservation(string foodId, string customerId)
        {
            Food food = ValidateFoodExistense(foodId);
            Customer customer = ValidateCustomerExistense(customerId);

            if (IsFoodReserved(foodId))
            {
                throw new AuthorizationException("Food item is already reserved.");
            }

            Restaurant restaurant = _restaurantRepository.GetById(food.IdRestaurant);

            Reservation reservation = new Reservation(null, false, food, restaurant, customer);
            return _reservationRepository.Insert(reservation);
        }

        public void CancelReservation(string foodId, string customerId)
        {
            ValidateFoodExistense(foodId);
            ValidateCustomerExistense(customerId);

            Reservation reservation = _reservationRepository.GetByFoodAndCustomer(foodId, customerId);
            _ = reservation ?? throw new EntityNotFoundException("No reservation was found.");
            
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

        public bool IsFoodReserved(string foodId)
        {
            return _reservationRepository.GetAll().ToList().Where(x => x.Food.Id == foodId).Count() > 0;
        }
    }
}
