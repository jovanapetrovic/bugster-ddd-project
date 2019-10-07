using System;
using System.Collections.Generic;

namespace Bugster.Business.Models
{
    public class AddProjectBugRequest
    {
        public long ProjectId { get; set; }
        public long Assignee { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public IEnumerable<long> Tags { get; set; }
    }
}