﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynlist.Data.Entities
{
    public class RecipeType
    {
        [Key]
        public int Id { get; set; }
        public string RecipeTypeName { get; set; }

    }
}
