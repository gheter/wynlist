using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynlist.Data.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemName { get; set; }


    }
}
