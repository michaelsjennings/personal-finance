using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MSJennings.PersonalFinance.Data.Services.EntityFramework
{
    public static class IServiceCollectionExtensions
    {
        #region Public Methods

        public static IServiceCollection AddEntityFrameworkDataServices(this IServiceCollection services, string connectionString)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICategoriesDataService, CategoriesDataService>();
            services.AddScoped<ITransactionsDataService, TransactionsDataService>();

            return services;
        }

        #endregion Public Methods
    }
}
