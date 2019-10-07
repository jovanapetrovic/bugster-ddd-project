using System;

namespace Bugster.Domain
{
    public class Bug
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public BugStatus Status { get; private set; }
        public BugPriority Priority { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime DateCreated { get; private set; }
        public long AssigneeId { get; private set; }
        public User Assignee { get; private set; }

        private Bug()
        {
        }

        public Bug(string title, DateTime dueDate)
        {
            Status = BugStatus.OPEN;
            Priority = BugPriority.MEDIUM;
            DateCreated = DateTime.UtcNow;

            Title = title;
            DueDate = dueDate;
        }
        
        public Bug(string title, BugStatus status, BugPriority priority, DateTime dueDate)
        {
            Title = title;
            Status = status;
            Priority = priority;
            DueDate = dueDate;

            DateCreated = DateTime.UtcNow;
        }

        public void UpdateTitleTo(string title)
        {
            Title = title;
        }
        
        public void UpdateDescriptionTo(string description)
        {
            Description = description;
        }
        
        public void UpdateStatusTo(BugStatus status)
        {
            Status = status;
        }
        
        public void UpdatePriorityTo(BugPriority priority)
        {
            Priority = priority;
        }
        
        public void UpdateDueDateTo(DateTime dueDate)
        {
            DueDate = dueDate;
        }

        public void AssignTo(User assingee)
        {
            if (assingee == default)
            {
                return;
            }

            Assignee = assingee;
        }
    }
}
