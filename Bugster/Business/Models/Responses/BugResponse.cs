﻿using System;

namespace Bugster.Business.Models
{
    public class BugResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public UserResponse Assignee { get; set; }
    }
}
