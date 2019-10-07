namespace Bugster.Business.Models
{
    public class RemoveProjectTeamMemberRequest
    {
        public long ProjectId { get; set; }
        public long TeamMemberId { get; set; }
    }
}