﻿using System.ComponentModel.DataAnnotations;

namespace AdminPanal.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage ="Name is Required!")]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
