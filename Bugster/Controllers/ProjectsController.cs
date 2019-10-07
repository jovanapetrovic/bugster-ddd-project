using Bugster.Business.Models;
using Bugster.Business.ProjectApplicationService;
using Bugster.Business.TagsApplicationService;
using Bugster.Business.UserApplicationService;
using Bugster.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectsController :
        Controller
    {
        private readonly IProjectApplicationService _projectApplicationService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly ITagApplicationService _tagApplicationService;

        public ProjectsController(IProjectApplicationService projectApplicationService,
            IUserApplicationService userApplicationService,
            ITagApplicationService tagApplicationService)
        {
            _projectApplicationService = projectApplicationService;
            _userApplicationService = userApplicationService;
            _tagApplicationService = tagApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var readProjectsRequest = new ReadProjectsRequest();
            var response = await _projectApplicationService.Handle(readProjectsRequest);

            return View(response);
        }

        public async Task<IActionResult> Details([BindRequired]long? projectId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var readProjectRequest = new ReadProjectRequest
            {
                ProjectId = projectId.Value
            };
            var project = await _projectApplicationService.Handle(readProjectRequest);
            var readAvailableTeamMembers = new ReadUsersRequest
            {
                Roles = new List<string> { "frontend", "backend", "tester" }
            };
            var availableTeamMembers = await _userApplicationService.Handle(readAvailableTeamMembers);

            var viewModel = new ProjectDetailsViewModel
            {
                Project = project,
                AvailableUsers = availableTeamMembers
                    .Select(user => new SelectListItem { Value = user.Id.ToString(), Text = user.FullName })
                    .ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> AddTeamMember([BindRequired]long? projectId, [BindRequired]long? teamMemberId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var addTeamMemberRequest = new AddProjectTeamMemberRequest
            {
                ProjectId = projectId.Value,
                TeamMemberId = teamMemberId.Value
            };
            await _projectApplicationService.Handle(addTeamMemberRequest);

            return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
        }

        public async Task<IActionResult> RemoveTeamMember([BindRequired]long? projectId, [BindRequired]long? teamMemberId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var removeTeamMemberRequest = new RemoveProjectTeamMemberRequest
            {
                ProjectId = projectId.Value,
                TeamMemberId = teamMemberId.Value
            };
            await _projectApplicationService.Handle(removeTeamMemberRequest);

            return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
        }

        public async Task<IActionResult> AddProjectBug([BindRequired] long? projectId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var readProjectRequest = new ReadProjectRequest
            {
                ProjectId = projectId.Value
            };
            var foundProject = await _projectApplicationService.Handle(readProjectRequest);
            var readAvailableTagsRequest = new ReadTagsRequest();
            var availableTags = await _tagApplicationService.Handle(readAvailableTagsRequest);

            var viewModel = new AddProjectBugViewModel
            {
                ProjectId = projectId,
                TeamMembers = foundProject.TeamMembers
                    .Select(user => new SelectListItem { Value = user.Id.ToString(), Text = user.FullName })
                    .ToList(),
                AvailableTags = availableTags
                    .Select(tag => new SelectListItem { Value = tag.Id.ToString(), Text = tag.Name, })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProjectBug([FromForm, Bind("ProjectId,Priority,Title,DueDate,Description,Assignee,SelectedTags")] AddProjectBugViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var readProjectRequest = new ReadProjectRequest
                {
                    ProjectId = model.ProjectId.Value
                };
                var foundProject = await _projectApplicationService.Handle(readProjectRequest);
                var readAvailableTagsRequest = new ReadTagsRequest();
                var availableTags = await _tagApplicationService.Handle(readAvailableTagsRequest);

                model.TeamMembers = foundProject.TeamMembers
                    .Select(user => new SelectListItem { Value = user.Id.ToString(), Text = user.FullName })
                    .ToList();
                model.AvailableTags = availableTags
                    .Select(tag => new SelectListItem { Value = tag.Id.ToString(), Text = tag.Name })
                    .ToList();

                return View(model);
            }

            var addProjectBugRequest = new AddProjectBugRequest
            {
                ProjectId = model.ProjectId.Value,
                Priority = model.Priority,
                Title = model.Title,
                DueDate = model.DueDate.Value,
                Description = model.Description,
                Assignee = model.Assignee.Value,
                Tags = model.SelectedTags
            };
            await _projectApplicationService.Handle(addProjectBugRequest);

            return RedirectToAction(nameof(Details), new { projectId = model.ProjectId.Value });
        }

        public async Task<IActionResult> DetailProjectBug([BindRequired]long? projectId, [BindRequired]long? bugId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var readProjectBugRequest = new ReadProjectBugRequest
            {
                ProjectId = projectId.Value,
                BugId = bugId.Value
            };
            var bug = await _projectApplicationService.Handle(readProjectBugRequest);

            var viewModel = new ProjectBugDetailViewModel
            {
                ProjectId = projectId.Value,
                Bug = bug
            };

            return View(viewModel);
        }

        public async Task<IActionResult> UpdateProjectBug([BindRequired]long? projectId, [BindRequired]long? bugId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var readProjectRequest = new ReadProjectRequest
            {
                ProjectId = projectId.Value
            };
            var foundProject = await _projectApplicationService.Handle(readProjectRequest);
            var readProjectBugRequest = new ReadProjectBugRequest
            {
                ProjectId = projectId.Value,
                BugId = bugId.Value
            };
            var foundBug = await _projectApplicationService.Handle(readProjectBugRequest);
            var viewModel = new UpdateProjectBugViewModel
            {
                Assignee = foundBug.Assignee.Id,
                ProjectId = projectId,
                BugId = bugId,
                TeamMembers = foundProject.TeamMembers
                    .Select(user => new SelectListItem { Value = user.Id.ToString(), Text = user.FullName })
                    .ToList(),
                Description = foundBug.Description,
                DueDate = foundBug.DueDate,
                Priority = foundBug.Priority,
                Status = foundBug.Status,
                Title = foundBug.Title
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProjectBug([FromForm, Bind("ProjectId,BugId,Assignee,Title,Description,Priority,DueDate,Status")] UpdateProjectBugViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var readProjectRequest = new ReadProjectRequest
                {
                    ProjectId = model.ProjectId.Value
                };
                var foundProject = await _projectApplicationService.Handle(readProjectRequest);
                model.TeamMembers = foundProject.TeamMembers
                    .Select(user => new SelectListItem { Value = user.Id.ToString(), Text = user.FullName })
                    .ToList();

                return RedirectToAction(nameof(Details), new { projectId = model.ProjectId.Value });
            }

            var updateProjectBugRequest = new UpdateProjectBugRequest
            {
                ProjectId = model.ProjectId.Value,
                BugId = model.BugId.Value,
                DueDate = model.DueDate.Value,
                Description = model.Description,
                Title = model.Title,
                Priority = model.Priority,
                Status = model.Status,
                Assignee = model.Assignee.Value
            };
            await _projectApplicationService.Handle(updateProjectBugRequest);

            return RedirectToAction(nameof(Details), new { projectId = model.ProjectId.Value });
        }

        public async Task<IActionResult> RemoveProjectBug([BindRequired]long? projectId, [BindRequired]long? bugId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
            }

            var removeProjectBugRequest = new RemoveProjectBugRequest
            {
                ProjectId = projectId.Value,
                BugId = bugId.Value
            };
            await _projectApplicationService.Handle(removeProjectBugRequest);

            return RedirectToAction(nameof(Details), new { projectId = projectId.Value });
        }

        public async Task<IActionResult> Create()
        {
            var readManagerUserRequest = new ReadUsersRequest
            {
                Roles = new List<string> { "manager" }
            };
            var managers = await _userApplicationService.Handle(readManagerUserRequest);

            var viewModel = new CreateProjectViewModel
            {
                Managers = managers
                    .Select(manager => new SelectListItem
                    {
                        Value = manager.Id.ToString(),
                        Text = manager.FullName
                    })
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm, Bind("Name,Description,ManagerId")] CreateProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var readManagerUserRequest = new ReadUsersRequest
                {
                    Roles = new List<string> { "manager" }
                };
                var managers = await _userApplicationService.Handle(readManagerUserRequest);

                model.Managers = managers
                    .Select(manager => new SelectListItem
                    {
                        Value = manager.Id.ToString(),
                        Text = manager.FullName
                    })
                    .ToList();

                return View(model);
            }

            var createProjectRequest = new CreateProjectRequest
            {
                Name = model.Name,
                Description = model.Description,
                ManagerUserId = model.ManagerId.Value
            };
            var response = await _projectApplicationService.Handle(createProjectRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(long projectId)
        {
            var readUserRequest = new ReadProjectRequest
            {
                ProjectId = projectId
            };
            var response = await _projectApplicationService.Handle(readUserRequest);
            var viewModel = response.Adapt<UpdateProjectViewModel>();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm, Bind("ProjectId,Name,Description")] UpdateProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updateProjectRequest = new UpdateProjectRequest
            {
                ProjectId = model.ProjectId.Value,
                Name = model.Name,
                Description = model.Description
            };
            var response = await _projectApplicationService.Handle(updateProjectRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long projectId)
        {
            var deleteProjectRequest = new DeleteProjectRequest
            {
                ProjectId = projectId
            };
            var response = await _projectApplicationService.Handle(deleteProjectRequest);

            return RedirectToAction(nameof(Index));
        }
    }
}
