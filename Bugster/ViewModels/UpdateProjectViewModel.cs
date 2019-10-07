using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bugster.ViewModels
{
    public class UpdateProjectViewModel
    {
        [Required]
        [HiddenInput]
        public long? ProjectId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
