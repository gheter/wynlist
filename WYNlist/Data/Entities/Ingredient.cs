using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynlist.Data.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        [Required]
        public int RecipeId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public string IngredientQty { get; set; }
        public string IngredientCost { get; set; }

    }
}
