using TiendaSoftware.DataBase;
using TiendaSoftware.Helpers;
using TiendaSoftware.Services;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogUNAH.API.Services;

namespace TiendaSoftware
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void configureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // Add DbContext

            services.AddDbContext<TiendaSoftwareContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            // Add custom services
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<ISoftwaresService, SoftwaresService>();
            services.AddTransient<IListsService, ListsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IAuthService, AuthService>(); // servicio de autentificacion

            // Add AutoMapper

            services.AddAutoMapper(typeof(AutoMapperProfile));

            // CORS Configuration
            services.AddCors(opt =>
            {
                var allowURLS = Configuration.GetSection("AllowURLS").Get<string[]>();

                opt.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(allowURLS)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
