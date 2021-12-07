using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Interfaces;
using Services.Interfaces;
using Services.Options;
using WebApi.Options;
using Services.Services;
using System;
using System.IO;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Helpers;
using Services.Repositories;
using WebApi.Middleware;
using Serilog;
using Microsoft.AspNetCore.Http;

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
            services.Configure<TokenOptions>(
                options =>
                {
                    options.SecurityKey = Configuration["TokenOptions:SecurityKey"];
                }
            );
            services.Configure<GoogleOptions>(
                options =>
                {
                    options.ClientId = Configuration["GoogleOptions:ClientId"];
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

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);

            services.AddScoped<IFoodRepository, FoodEFRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantEFRepository>();
            services.AddScoped<ITypeOfFoodRepository, TypeOfFoodEFRepository>();
        }

        private void ConfigureLogger(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            services.AddSingleton(x => Log.Logger);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogger(services);
            ConfigureOptions(services);
            ConfigureDatabase(services);

            services.AddScoped<ITokenHelper, TokenHelper>();

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

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "JwtBearer";
            //    options.DefaultChallengeScheme = "JwtBearer";
            //})
            //    .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            //    {
            //        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:SecurityKey"])),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.FromMinutes(5)
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($@"SecretName (Name in Key Vault: 'SecretName'){Environment.NewLine}Obtained from Configuration with Configuration[""SecretName""]{Environment.NewLine}Value: {Configuration["SecretName"]}{Environment.NewLine}{Environment.NewLine}Section:SecretName (Name in Key Vault: 'EmailOptions--Host'){Environment.NewLine}Obtained from Configuration with Configuration[""EmailOptions:Host""]{Environment.NewLine}Value: {Configuration["EmailOptions:Host"]}{Environment.NewLine}{Environment.NewLine}Section:SecretName (Name in Key Vault: 'Section--SecretName'){Environment.NewLine}Obtained from Configuration with Configuration.GetSection(""Section"")[""SecretName""]{Environment.NewLine}Value: {Configuration.GetSection("Section")["SecretName"]}");
            });
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

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
