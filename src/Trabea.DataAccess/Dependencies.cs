using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trabea.DataAccess.Models;

namespace Basilisk.DataAccess {
    public static class Dependencies {

        public static void AddSqlServerService(IServiceCollection service, IConfiguration configuration) {
            service.AddSqlServer<TrabeaContext>(configuration.GetConnectionString("TrabeaConnection"));
        }
    }
}
