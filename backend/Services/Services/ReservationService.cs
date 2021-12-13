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
            Food food = _foodRepository.GetById(foodId);
            Customer customer = _cutomerRepository.GetById(customerId);

            if (food == null)
            {
                throw new EntityNotFoundException("Food with given id was not found.");
            } 
            else if (customer == null)
            {
                throw new EntityNotFoundException("Customer with given id was not found.");
            }

            if (IsFoodReserved(foodId))
            {
                throw new AuthorizationException("Food item is already reserved.");
            }

            Restaurant restaurant = _restaurantRepository.GetById(food.IdRestaurant);

            Reservation reservation = new Reservation(null, false, food, restaurant, customer);
            return _reservationRepository.Insert(reservation);
        }

        public bool IsFoodReserved(string foodId)
        {
            return _reservationRepository.GetAll().ToList().Where(x => x.Food.Id == foodId).Count() > 0;
        }
    }
}
