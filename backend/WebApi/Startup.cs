﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Interfaces;
using Persistence.Repositories;
using Services.Interfaces;
using Services.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureDatabase(IServiceCollection services)
        {

            services.AddScoped<IFoodRepository,FoodRepository>( _ =>
                new FoodRepository(DBConfiguration.Instance.PathToFoodsFile)
            );
            services.AddScoped<IRestaurantRepository, RestaurantRepository>(_ =>
                new RestaurantRepository(DBConfiguration.Instance.PathToRestaurantsFile)
            );
            services.AddScoped<ITypeOfFoodRepository, TypeOfFoodRepository>(_ =>
                new TypeOfFoodRepository(DBConfiguration.Instance.PathToTypesOfFoodFile)
            );
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<ITypeOfFoodService, TypeOfFoodService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "v1",
                    new OpenApiInfo { Title = "Wasted API", Version = "v1" }
                );
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
                app.UseSwaggerUI(c => {
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
