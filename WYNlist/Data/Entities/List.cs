using System.ComponentModel.DataAnnotations;

namespace Wynlist.Data.Entities
{
    public class List
    {
        [Key]
        public int Id { get; set; }
        public WynUser User { get; set; }
        public ListType ListType { get; set; }
        public int ListTypeId { get; set; }
        public string ListName { get; set; }

    }
}