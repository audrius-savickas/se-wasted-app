using Domain.Models;

namespace Contracts.DTOs
{
    public class RestaurantRegisterRequest
    {
        public string Name { get; set; }
        public Coords Coords { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }

        private const string DEFAULT_IMAGE_URL = "https://genesisairway.com/wp-content/uploads/2019/05/no-image.jpg";


        public RestaurantRegisterRequest(string name, string address, Coords coords, string phone, string description = "", string imageURL = "")
        {
            Phone = phone;
            Name = name;
            Address = address;
            Coords = coords;
            Description = description;
            if (imageURL == "")
            {
                ImageURL = DEFAULT_IMAGE_URL;
            }
            else
            {
                ImageURL = imageURL;
            }
        }
    }
}
