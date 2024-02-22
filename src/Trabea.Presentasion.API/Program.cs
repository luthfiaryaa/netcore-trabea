using Basilisk.DataAccess;
using Trabea.Business.Implementations;
using Trabea.Business.Interfaces;
using Trabea.Presentasion.API.Accounts;

namespace Trabea.Presentasion.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            Dependencies.AddSqlServerService(builder.Services, builder.Configuration);
            builder.Services.AddScoped<IPartTimeRepository, PartTimeRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();


            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();

            //app.UseAuthorization();

            app.Run();
        }
    }
}
