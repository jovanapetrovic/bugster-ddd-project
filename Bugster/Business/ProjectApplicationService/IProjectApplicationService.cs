using Bugster.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Business.ProjectApplicationService
{
    public interface IProjectApplicationService
    {
        Task<IEnumerable<ProjectResponse>> Handle(ReadProjectsRequest request);
        Task<ProjectResponse> Handle(ReadProjectRequest request);
        Task<ProjectResponse> Handle(CreateProjectRequest request);
        Task<ProjectResponse> Handle(UpdateProjectRequest request);
        Task<ProjectResponse> Handle(DeleteProjectRequest request);

        Task<UserResponse> Handle(AddProjectTeamMemberRequest request);
        Task<UserResponse> Handle(RemoveProjectTeamMemberRequest request);

        Task<IEnumerable<BugResponse>> Handle(ReadProjectBugsRequest request);
        Task<BugResponse> Handle(ReadProjectBugRequest request);
        Task<BugResponse> Handle(AddProjectBugRequest request);
        Task<BugResponse> Handle(UpdateProjectBugRequest request);
        Task<BugResponse> Handle(RemoveProjectBugRequest request);
    }
}
