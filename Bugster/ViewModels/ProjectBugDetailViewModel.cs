using Bugster.Business.Models;

namespace Bugster.ViewModels
{
    public class ProjectBugDetailViewModel
    {
        public BugResponse Bug { get; set; }
        public long ProjectId { get; set; }
    }
}
