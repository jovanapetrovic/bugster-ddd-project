namespace Bugster.Business.Models
{
    public class AddProjectTeamMemberRequest
    {
        public long ProjectId { get; set; }
        public long TeamMemberId { get; set; }
    }
}