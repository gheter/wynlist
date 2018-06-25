using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wynlist.Data.Entities;

namespace Wynlist.Data
{
    public class WynlistContext : IdentityDbContext<WynUser>
    {
        public WynlistContext(DbContextOptions<WynlistContext> options) : base(options)
        {
        }

        public DbSet<List> Lists { get; set; }
        public DbSet<ListType> ListTypes { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeType> RecipeTypes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemsCategories { get; set; }

        public DbSet<ListItem> ListItems { get; set; }

        public DbSet<Store> Stores { get; set; }
    }
}