using _4DOG.Data;
using _4DOG.Middlewares;
using _4DOG.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _4DOG
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
            services.AddHttpContextAccessor();
            services.AddControllers(); // כל השירותים של המערכת לדוגמא שירות של בסיס הנתונים

            string connectionString = Configuration.GetConnectionString("4Dog");
            services.AddDbContext<_4DogsDBContext>(options => // קישור למבנה הנתונים
            options.UseSqlServer(connectionString));

            services.AddAuthentication(options => //נכנס רק אם הוא מגיע עם מפתח 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = JwtService.TokenValidationParameters;
             });

            services.AddTransient<UsersServices>(); 
            services.AddTransient<DogOwnerServices>();
            services.AddTransient<DogRaceServices>();
            services.AddTransient<CitysServices>();
            services.AddTransient<TypesHaircutServices>();
            services.AddTransient<TypesHaircutServices>(); 
            services.AddTransient<TrainingPackageServices>();
            services.AddTransient<PensionServices>();
            services.AddTransient<BarberShopServices>();
            services.AddTransient<TrainingServices>();
            services.AddTransient<ShopsServices>();
            services.AddTransient<LodgingServices>();
            services.AddTransient<HaircutsServices>();
            services.AddTransient<DogTrainingServices>();
            services.AddTransient<JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // מבנה האפליקציה
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true));// allow any origin

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<AllowedCorsMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
