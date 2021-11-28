using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.File;
using Persistence.File.Interfaces;
using Persistence.File.Repositories;
using Persistence.EF;
using Services.Interfaces;
using Services.Options;
using Services.Services;
using System;
using System.IO;
using System.Reflection;
using Services.Repositories;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureOptions(IServiceCollection services)  
        {
            services.Configure<EmailOptions>(
                options =>
                {
                    options.Host = Configuration["EmailOptions:Host"];
                    options.UserName = Configuration["EmailOptions:UserName"];
                    options.Password = Configuration["EmailOptions:Password"];
                    options.Port = int.Parse(Configuration["EmailOptions:Port"]);
                }
            );
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("AppDatabase");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string not found");
            }

            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);


            /*services.AddScoped<IFoodRepository, FoodRepository>(_ =>
                new FoodRepository(DBConfiguration.Instance.PathToFoodsFile)
            );*/
            services.AddScoped<IFoodRepository, FoodEFRepository>();
            /*services.AddScoped<IRestaurantRepository, RestaurantRepository>(_ =>
                new RestaurantRepository(DBConfiguration.Instance.PathToRestaurantsFile)
            );*/
            services.AddScoped<IRestaurantRepository, RestaurantEFRepository>();
            services.AddScoped<ITypeOfFoodRepository, TypeOfFoodRepository>(_ =>
                new TypeOfFoodRepository(DBConfiguration.Instance.PathToTypesOfFoodFile)
            );
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOptions(services);
            ConfigureDatabase(services);

            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<ITypeOfFoodService, TypeOfFoodService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "v1",
                    new OpenApiInfo { Title = "Wasted API", Version = "v1" }
                );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint
                    (
                        "/swagger/v1/swagger.json",
                        "Wasted API v1"
                    );
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
