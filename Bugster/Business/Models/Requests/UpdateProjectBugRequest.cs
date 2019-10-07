using System;

namespace Bugster.Business.Models
{
    public class UpdateProjectBugRequest
    {
        public long ProjectId { get; set; }
        public long BugId { get; set; }
        public string Title { get; set; }
        public long Assignee { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}