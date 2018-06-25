using System;
using System.ComponentModel.DataAnnotations;

namespace Wynlist.Data.Entities
{
    public class ListType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ListTypeName { get; set; }


    }
}