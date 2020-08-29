using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace recipe_api.Models
{
    public class RecipeDBContext:DbContext
    {
        public RecipeDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().Property(e => e.Ingredients).HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            modelBuilder.Entity<Recipe>().Property(e => e.Steps).HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            modelBuilder.Entity<Recipe>()
            .Property(r => r.CreatedDate)
            .HasDefaultValueSql("getdate()");

        }
    }
}
