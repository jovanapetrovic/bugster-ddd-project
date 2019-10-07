using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugster.ViewModels
{
    public class UpdateProjectBugViewModel
    {
        [Required]
        [HiddenInput]
        public long? ProjectId { get; set; }

        [Required]
        [HiddenInput]
        public long? BugId { get; set; }

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

        [Display(Name = "Priority")]
        [Required]
        public string Priority { get; set; }

        [Display(Name = "Due date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        public List<SelectListItem> Priorities { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "LOWEST", Text = "Lowest" },
            new SelectListItem { Value = "LOW", Text = "Low" },
            new SelectListItem { Value = "MEDIUM", Text = "Medium" },
            new SelectListItem { Value = "HIGH", Text = "High" },
            new SelectListItem { Value = "HIGHEST", Text = "Highest" },
        };

        public List<SelectListItem> Statuses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "OPEN", Text = "Open" },
            new SelectListItem { Value = "IN_PROGRESS", Text = "In Progress" },
            new SelectListItem { Value = "TESTING", Text = "Testing" },
            new SelectListItem { Value = "CLOSED", Text = "Closes" }
        };

        public List<SelectListItem> TeamMembers { get; set; }
    }
}
