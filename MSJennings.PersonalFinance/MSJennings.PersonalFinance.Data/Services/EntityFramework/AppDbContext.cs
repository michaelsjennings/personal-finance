using Microsoft.EntityFrameworkCore;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.Data.Services.EntityFramework
{
    public class AppDbContext : DbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        #endregion Public Properties
    }
}
