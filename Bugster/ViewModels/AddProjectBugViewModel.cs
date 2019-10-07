using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugster.ViewModels
{
    public class AddProjectBugViewModel
    {
        [Required]
        [HiddenInput]
        public long? ProjectId { get; set; }

        [Display(Name = "Assignee")]
        [Required]
        public long? Assignee { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; } = "MEDIUM";

        [Required]
        [Display(Name = "Tags")]
        public IEnumerable<long> SelectedTags { get; set; }

        [Required]
        [Display(Name = "Due date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public List<SelectListItem> Priorities { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "LOWEST", Text = "Lowest" },
            new SelectListItem { Value = "LOW", Text = "Low" },
            new SelectListItem { Value = "MEDIUM", Text = "Medium" },
            new SelectListItem { Value = "HIGHEST", Text = "High" },
            new SelectListItem { Value = "HIGHEST", Text = "Highest" },
        };

        public List<SelectListItem> TeamMembers { get; set; }
        public List<SelectListItem> AvailableTags { get; set; }
    }
}
