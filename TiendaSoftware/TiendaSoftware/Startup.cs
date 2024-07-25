using TiendaSoftware.DataBase;
using TiendaSoftware.Helpers;
using TiendaSoftware.Services;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TiendaSoftware
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public void configureServices(IServiceCollection services) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // Add DbContext

            services.AddDbContext<TiendaSoftwareContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add custom services
            services.AddTransient<IPublisherService, PublisherService>(); // servicio de categorias
            services.AddTransient<IAuthService, AuthService>(); // servicio de autentificacion

            // Add AutoMapper

            services.AddAutoMapper(typeof(AutoMapperProfile));

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
