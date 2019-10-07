using Bugster.Business.Models;
using Bugster.Domain;
using Bugster.Repositories.ProjectRepository;
using Bugster.Repositories.UserRepository;
using Mapster;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Business.ProjectApplicationService
{
    public class ProjectApplicationService :
        IProjectApplicationService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskAssigner _bugAutoAssigner;

        public ProjectApplicationService(IProjectRepository projectRepository,
            IUserRepository userRepository,
            ITaskAssigner bugAutoAssigner)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _bugAutoAssigner = bugAutoAssigner;
        }

        public async Task<IEnumerable<ProjectResponse>> Handle(ReadProjectsRequest request)
        {
            var projects = await _projectRepository.ReadAll();

            var response = projects.Select(project => project.Adapt<ProjectResponse>());
            return response;
        }

        public async Task<ProjectResponse> Handle(ReadProjectRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException(nameof(project));
            }

            return project.Adapt<ProjectResponse>();
        }

        public async Task<ProjectResponse> Handle(CreateProjectRequest request)
        {
            var manager = await _userRepository.ReadById(request.ManagerUserId);
            if (manager == default)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var project = new Project(request.Name, manager);
            project.UpdateDescriptionTo(request.Description);

            _projectRepository.Create(project);
            await _projectRepository.PersistChanges();

            return project.Adapt<ProjectResponse>();
        }

        public async Task<ProjectResponse> Handle(UpdateProjectRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException(nameof(project));
            }

            project.UpdateNameTo(request.Name);
            project.UpdateDescriptionTo(request.Description);

            await _projectRepository.PersistChanges();

            return project.Adapt<ProjectResponse>();
        }

        public async Task<ProjectResponse> Handle(DeleteProjectRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException(nameof(project));
            }

            project.Manager.UpdateStatusTo(UserStatus.INACTIVE);
            await _userRepository.PersistChanges();

            _projectRepository.Delete(project);
            await _projectRepository.PersistChanges();

            return project.Adapt<ProjectResponse>();
        }

        public async Task<IEnumerable<BugResponse>> Handle(ReadProjectBugsRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }

            var bugs = project.Bugs
                .Select(bug => bug.Adapt<BugResponse>())
                .ToList();
            return bugs;
        }

        public async Task<BugResponse> Handle(ReadProjectBugRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }

            var foundBug = project.Bugs
                .FirstOrDefault(bug => bug.Id == request.BugId);
            if (foundBug == default)
            {
                throw new ArgumentNullException("Bug for this project not found");
            }

            return foundBug.Adapt<BugResponse>();
        }

        public async Task<BugResponse> Handle(AddProjectBugRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }
            if (!Enum.IsDefined(typeof(BugPriority), request.Priority.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("Bug priority has wrong value");
            }

            var priority = Enum.Parse<BugPriority>(request.Priority.ToUpper());

            var bug = new Bug(request.Title, BugStatus.OPEN, priority, request.DueDate);
            bug.UpdateDescriptionTo(request.Description);
            long assignee = request.Assignee;
            if (request.Assignee == 0) // Should auto assign
            {
                assignee = await _bugAutoAssigner.Propose(project, request.Tags);
            }
            
            project.CreateAndAssignBug(bug, assignee);

            await _projectRepository.PersistChanges();
            return bug.Adapt<BugResponse>();
        }

        public async Task<BugResponse> Handle(UpdateProjectBugRequest request)
        {
            if (!Enum.IsDefined(typeof(BugPriority), request.Priority.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("Bug priority has wrong value");
            }
            if (!Enum.IsDefined(typeof(BugStatus), request.Status.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("Bug status has wrong value");
            }

            var priority = Enum.Parse<BugPriority>(request.Priority.ToUpper());
            var status = Enum.Parse<BugStatus>(request.Status.ToUpper());

            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }

            var bug = project.Bugs.SingleOrDefault(bug => bug.Id == request.BugId);
            if (bug == default)
            {
                throw new ArgumentNullException("Bug for this project not found");
            }

            var assignedTeamMember = project.TeamMembers.FirstOrDefault(user => user.Id == request.Assignee);

            bug.AssignTo(assignedTeamMember);
            bug.UpdateTitleTo(request.Title);
            bug.UpdateDescriptionTo(request.Description);
            bug.UpdateStatusTo(status);
            bug.UpdatePriorityTo(priority);
            bug.UpdateDueDateTo(request.DueDate);

            await _projectRepository.PersistChanges();
            return bug.Adapt<BugResponse>();
        }

        public async Task<BugResponse> Handle(RemoveProjectBugRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }
            
            var bug = project.Bugs.SingleOrDefault(bug => bug.Id == request.BugId);
            if (bug == default)
            {
                throw new ArgumentNullException("Bug for this project not found");
            }

            project.RemoveBug(bug);

            await _projectRepository.PersistChanges();
            return bug.Adapt<BugResponse>();
        }

        public async Task<UserResponse> Handle(AddProjectTeamMemberRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }

            var teamMember = await _userRepository.ReadById(request.TeamMemberId);
            if (project == default)
            {
                throw new ArgumentNullException("User with id not found");
            }

            project.AddTeamMember(teamMember);

            await _projectRepository.PersistChanges();
            return teamMember.Adapt<UserResponse>();
        }

        public async Task<UserResponse> Handle(RemoveProjectTeamMemberRequest request)
        {
            var project = await _projectRepository.ReadById(request.ProjectId);
            if (project == default)
            {
                throw new ArgumentNullException("Project not found");
            }

            var teamMember = project.TeamMembers.FirstOrDefault(user => user.Id == request.TeamMemberId);
            if(teamMember == default)
            {
                throw new ArgumentNullException("User with id not found");
            }

            project.RemoveTeamMember(teamMember);

            await _projectRepository.PersistChanges();
            return teamMember.Adapt<UserResponse>();
        }
    }
}
