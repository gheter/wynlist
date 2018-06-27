using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Wynlist.Data.Entities
{
    public class Recipe
    {

        [Key]
        public int Id { get; set; }
        public WynUser User { get; set; }
        public RecipeType RecipeType { get; set; }
        public int RecipeTypeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public DateTime RecipeDate { get; set; }

    }
}
