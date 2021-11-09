using MailKit.Net.Smtp;
using MimeKit;
using Persistence;
using Persistence.Repositories;
using Services.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbConfiguration = DBConfiguration.Instance;

            // Initialize repositories
            var typeOfFoodRepository = new TypeOfFoodRepository
            (
                dbConfiguration.PathToTypesOfFoodFile
            );
            var foodRepository = new FoodRepository
            (
                dbConfiguration.PathToFoodsFile
            );
            var restaurantRepository = new RestaurantRepository
            (
                dbConfiguration.PathToRestaurantsFile
            );

            // Initialize services
            var typeOfFoodService = new TypeOfFoodService
            (
                typeOfFoodRepository
            );
            var foodService = new FoodService
            (
                foodRepository,
                restaurantRepository,
                typeOfFoodRepository
            );
            var restaurantService = new RestaurantService
            (
                restaurantRepository,
                foodRepository
            );

            var emailService = new EmailService();

            // Usage

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Wasted App Team", "wasted.app.team@gmail.com"));
            mailMessage.To.Add(MailboxAddress.Parse("wasted.app.team@gmail.com"));
            mailMessage.Subject = "subject";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Hello"
            };

            emailService.Send(mailMessage);
            
        }
    }
}
