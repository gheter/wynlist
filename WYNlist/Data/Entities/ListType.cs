using System;
using System.ComponentModel.DataAnnotations;

namespace Wynlist.Data.Entities
{
    public class ListType
    {
        [Key]
        public int Id { get; set; }
        public string ListTypeName { get; set; }


    }
}