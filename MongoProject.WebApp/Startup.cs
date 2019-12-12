using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDbGenericRepository;
using MongoProject.WebApp.Data;
using MongoProject.WebApp.Data.Models;

namespace MongoProject.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoDbContext = new MongoDbContext(Configuration.GetConnectionString("DatabaseConnectionString"), "MongoProject");

            services.AddIdentity<User, ApplicationRole>()
                .AddMongoDbStores<IMongoDbContext>(mongoDbContext)
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;

                //Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                //SignIn settings
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Login";
                options.LogoutPath = $"/Logout";
            });

            BsonClassMap.RegisterClassMap<Component>();
            BsonClassMap.RegisterClassMap<BLDCMotor>();
            BsonClassMap.RegisterClassMap<Bolt>();
            BsonClassMap.RegisterClassMap<Microcontroller>();
            BsonClassMap.RegisterClassMap<Propeller>();
            BsonClassMap.RegisterClassMap<StepperMotor>();
            BsonClassMap.RegisterClassMap<Kit>();

            services.AddScoped(x => mongoDbContext.Database);

            services.AddScoped<IRepository, MongoRepository>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
