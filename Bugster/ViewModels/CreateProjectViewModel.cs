using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bugster.ViewModels
{
    public class CreateProjectViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Manager")]
        [Required]
        public long? ManagerId { get; set; }

        public List<SelectListItem> Managers { get; set; }
    }
}
