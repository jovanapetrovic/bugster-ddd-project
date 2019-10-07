using Bugster.Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bugster.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectResponse Project { get; set; }
        public List<SelectListItem> AvailableUsers { get; set; }
    }
}
