namespace Bugster.Business.Models
{
    public class RemoveProjectBugRequest
    {
        public long ProjectId { get; set; }
        public long BugId { get; set; }
    }
}