namespace Bugster.Business.Models
{
    public class ReadProjectBugRequest
    {
        public long ProjectId { get; set; }
        public long BugId { get; set; }
    }
}