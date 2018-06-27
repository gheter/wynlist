using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynlist.Data.Entities
{
    public class ListItem
    {
        [Key]
        public int Id { get; set; }
        public WynUser User { get; set; }
        public List List { get; set; }
        public int ListId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        //I need fields for qty, cost, store, date that should come from the Ingredient and Recipe tables but then exist in this table independently
        public  string ListItemQty { get; set; }
        public string ListItemCost { get; set; }
        public int ListItemStoreId { get; set; }
        public int ListItemChecked { get; set; }
        public int ListItemPriority { get; set; }

    }
}
