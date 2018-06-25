using System.ComponentModel.DataAnnotations;

namespace Wynlist.Data.Entities
{
    public class List
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public WynUser User { get; set; }
        public ListType ListType { get; set; }
        public int ListTypeId { get; set; }
        [Required]
        public string ListName { get; set; }

    }
}