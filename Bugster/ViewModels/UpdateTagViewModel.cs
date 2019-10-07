using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bugster.ViewModels
{
    public class UpdateTagViewModel
    {
        [Required]
        [HiddenInput]
        public long? TagId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Tag role")]
        [Required]
        public string Bound { get; set; }

        public List<SelectListItem> BoundRoles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "FRONTEND", Text = "Front end developer" },
            new SelectListItem { Value = "BACKEND", Text = "Back end developer" }
        };
    }
}
