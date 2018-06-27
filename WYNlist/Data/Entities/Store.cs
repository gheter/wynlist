using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynlist.Data.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string StoreName { get; set; }

    }
}
