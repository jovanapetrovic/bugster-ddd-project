namespace Bugster.Business.Models
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ManagerUserId { get; set; }
    }
}