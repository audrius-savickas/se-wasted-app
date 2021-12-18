using Contracts.DTOs;
using Domain.Models;
using Domain.Helpers;
using Persistence.Interfaces;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Linq;
using Domain.Models.QueryParameters;
using Domain.Models.Extensions;
using Services.Utils;
using Persistence.Utils;
using Services.Mappers;

namespace Services.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IFoodRepository _foodRepository;

        public event EventHandler<RestaurantEventArgs> RestaurantRegistered;

        public RestaurantService
        (
            IRestaurantRepository restaurantRepository,
            IFoodRepository foodRepository
        )
        {
            _restaurantRepository = restaurantRepository;
            _foodRepository = foodRepository;
        }

        public void ChangePass(Mail mail, Password newPassword)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid email.");
            }

            string error = Validator.ValidatePassword(newPassword.Value);
            if (error != "")
            {
                throw new ArgumentException(error);
            }

            Credentials creds = restaurant.Credentials;
            newPassword.Value = PasswordHasher.Hash(newPassword.Value);
            creds.Password = newPassword;
            _restaurantRepository.Update(restaurant);
        }

        public RestaurantDto GetRestaurantDtoFromMail(Mail mail, Coords coords = null)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(mail);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid email.");
            }

            return restaurant.ToDTO(coords);
        }

        public void DeleteAccount(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);

            if (restaurant == null)
            {
                throw new EntityNotFoundException("The restaurant does not exist");
            }

            if (!PasswordHasher.Verify(creds.Password.Value, restaurant.Credentials.Password.Value))
            {
                throw new AuthorizationException("The credentials are not correct");
            }

            _restaurantRepository.Delete(restaurant.Id);
        }

        public PagedList<RestaurantDto> GetAllRestaurants(RestaurantParameters restaurantParameters)
        {
            var coords = new Coords { Longitude = restaurantParameters.Longitude, Latitude = restaurantParameters.Latitude };
            var restaurantPagedList = PagedList<Restaurant>.ToPagedList(
                _restaurantRepository.GetAll().AsEnumerable().SortRestaurants(restaurantParameters.SortOrder, coords),
                restaurantParameters.PageNumber,
                restaurantParameters.PageSize);
            return restaurantPagedList.ConvertAllItems(r => r.ToDTO(coords));
        }

        public RestaurantDto GetRestaurantById(string idRestaurant, Coords coords = null)
        {
            Restaurant restaurant = _restaurantRepository.GetById(idRestaurant);
            if (restaurant == null)
            {
                throw new EntityNotFoundException("Invalid restaurant id.");
            }

            return restaurant.ToDTO(coords);
        }

        public PagedList<RestaurantDto> GetRestaurantsNear(RestaurantParameters restaurantParameters)
        {
            var coords = new Coords { Longitude = restaurantParameters.Longitude, Latitude = restaurantParameters.Latitude };
            var restaurantPagedList = PagedList<Restaurant>.ToPagedList(
                _restaurantRepository.GetRestaurantsNear(coords).AsEnumerable()
                                     .SortRestaurants(restaurantParameters.SortOrder, coords),
                restaurantParameters.PageNumber,
                restaurantParameters.PageSize);
            return restaurantPagedList.ConvertAllItems(r => r.ToDTO(coords));
        }

        public bool Login(Credentials creds)
        {
            Restaurant restaurant = _restaurantRepository.GetByMail(creds.Mail);
            return restaurant != null && restaurant.Credentials.Mail.Value == creds.Mail.Value && PasswordHasher.Verify(creds.Password.Value, restaurant.Credentials.Password.Value);
        }

        public string Register(Credentials creds, RestaurantRegisterRequest restaurantRegisterRequest)
        {

            // Validations
            if (_restaurantRepository.GetByMail(creds.Mail) != null)
            {
                throw new AuthorizationException("There is already an account registered on this mail");
            }

            string error = Validator.ValidateEmail(creds.Mail.Value) + Validator.ValidatePassword(creds.Password.Value);

            if (error != "")
            {
                throw new ArgumentException(error);
            }

            Restaurant restaurant = new Restaurant
            {
                Phone = restaurantRegisterRequest.Phone,
                Name = restaurantRegisterRequest.Name,
                Address = restaurantRegisterRequest.Address,
                Coords = restaurantRegisterRequest.Coords,
                Credentials = new Credentials(creds.Mail.Value, PasswordHasher.Hash(creds.Password.Value)),
                ImageURL = restaurantRegisterRequest.ImageURL,
                Description = restaurantRegisterRequest.Description,
            };

            string id = _restaurantRepository.Insert(restaurant);

            // Raise an event that restaurant has registered
            OnRestaurantRegistered(new RestaurantEventArgs(restaurant));

            return id;
        }

        protected virtual void OnRestaurantRegistered(RestaurantEventArgs e)
        {
            RestaurantRegistered?.Invoke(this, e);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            Restaurant restaurantDB = _restaurantRepository.GetById(restaurant.Id);

            if (restaurantDB == null)
            {
                throw new EntityNotFoundException("Invalid restaurant id.");
            }

            restaurant.Credentials = restaurantDB.Credentials;
            _restaurantRepository.Update(restaurant);
        }

        public PagedList<RestaurantDto> GetAllRestaurantsCloserThan(RestaurantParameters restaurantParameters, Distances distance)
        {
            var coords = new Coords { Longitude = restaurantParameters.Longitude, Latitude = restaurantParameters.Latitude };
            var restaurantPagedList = PagedList<Restaurant>.ToPagedList(
                _restaurantRepository.GetAllRestaurantsCloserThan(coords, distance).AsEnumerable()
                                     .SortRestaurants(restaurantParameters.SortOrder, coords),
                restaurantParameters.PageNumber,
                restaurantParameters.PageSize);
            return restaurantPagedList.ConvertAllItems(r => r.ToDTO(coords));
        }

        public PagedList<Food> GetAllFoodFromRestaurant(string idRestaurant, FoodParameters foodParameters, bool reserved)
        {
            var foodItems = _foodRepository.GetFoodFromRestaurant(idRestaurant).AsEnumerable();
            if (reserved)
            {
                foodItems = foodItems.Where(x => x.Reservation != null);
            }
            return PagedList<Food>.ToPagedList(
                foodItems.SortFood(foodParameters.SortOrder),
                foodParameters.PageNumber,
                foodParameters.PageSize);
        }

        public PagedList<Food> GetAllReservedFoodFromRestaurant(string idRestaurant, FoodParameters foodParameters)
        {
            return PagedList<Food>.ToPagedList(
                _foodRepository.GetFoodFromRestaurant(idRestaurant).AsEnumerable().Where(x => x.Reservation != null).SortFood(foodParameters.SortOrder),
                foodParameters.PageNumber,
                foodParameters.PageSize);
        }

        public int GetFoodCountFromRestaurant(string idRestaurant)
        {
            return _foodRepository.GetFoodFromRestaurant(idRestaurant).Count();
        }
    }

    public class RestaurantEventArgs : EventArgs
    {
        public Restaurant Restaurant { get; set; }

        public RestaurantEventArgs(Restaurant restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
