using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYNlist.ViewModels
{
    public class ListViewModel
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public int ListTypeId { get; set; }
    }
}
