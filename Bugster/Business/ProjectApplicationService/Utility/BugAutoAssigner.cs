using Bugster.Domain;
using Bugster.Repositories.TagRepository;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Business.ProjectApplicationService
{
    public class BugAutoAssigner :
        ITaskAssigner
    {
        private readonly ITagRepository _tagRepository;

        public BugAutoAssigner(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<long> Propose(Project project, IEnumerable<long> tags)
        {
            var allTags = await _tagRepository.ReadAll();
            var groupedByBoundRole = allTags.Where(tag => tags.Contains(tag.Id))
                .GroupBy(tag => tag.Bound)
                .ToList();

            var role = groupedByBoundRole.MaxBy(group => group.Count())
                .Select(group => group.Key)
                .SingleOrDefault();
            if (role == default)
            {
                throw new InvalidOperationException("Cannot find requirement role");
            }

            var proposedUserId = project.TeamMembers
                .Where(user => user.Role == role)
                .GroupJoin(project.Bugs, 
                    teamMember => teamMember.Id, 
                    bug => bug.AssigneeId, 
                    (teamMember, bugsCollection) => new { UserId = teamMember.Id, BugsCount = bugsCollection.Count() })
                .MinBy(userBugsEntity => userBugsEntity.BugsCount)
                .Select(userBugsEntity => userBugsEntity.UserId)
                .FirstOrDefault();

            if (proposedUserId == 0)
            {
                throw new InvalidOperationException("Cannot find user for this task");
            }

            return proposedUserId;
        }
    }
}
