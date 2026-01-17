using Microsoft.EntityFrameworkCore;
using FoodOrderApp.Models;

namespace FoodOrderApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // כאן אנחנו אומרים לו: "צור טבלה בשם Users לפי המודל שיצרנו"
        public DbSet<User> Users { get; set; }
    }
}