using Basilisk.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Trabea.Business.Implementations;
using Trabea.Business.Interfaces;
using Trabea.Presentasion.Web.Services.Implementations;
using Trabea.Presentasion.Web.Services.Interfaces;

namespace Trabea.Presentasion.Web {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            Dependencies.AddSqlServerService(builder.Services, builder.Configuration);
            //builder.Services.AddSqlServer<TrabeaContext>(builder.Configuration.GetConnectionString("TrabeaConnection"));

            builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            builder.Services.AddScoped<IPartTimeRepository, PartTimeRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();


            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPartTimeService, PartTimeService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => {
                    option.AccessDeniedPath = "/forbidden";
                    option.LoginPath = "/login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            var app = builder.Build();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}