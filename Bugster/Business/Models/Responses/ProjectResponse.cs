using System.Collections.Generic;

namespace Bugster.Business.Models
{
    public class ProjectResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UserResponse Manager { get; set; }
        public List<UserResponse> TeamMembers { get; set; }
        public List<BugResponse> Bugs { get; set; }
    }
}