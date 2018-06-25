using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYNlist.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; } 
        public bool RememberMe { get; set; } 
    }
}
