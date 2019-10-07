﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bugster.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        [HiddenInput]
        public long? UserId { get; set; }

        [Display(Name = "Email address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required]
        [StringLength(24, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Role")]
        [Required]
        public string Role { get; set; }

        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "MANAGER", Text = "Project manager" },
            new SelectListItem { Value = "FRONTEND", Text = "Front end developer" },
            new SelectListItem { Value = "BACKEND", Text = "Back end developer"  },
            new SelectListItem { Value = "TESTER", Text = "Quality assurance"  }
        };
    }
}
