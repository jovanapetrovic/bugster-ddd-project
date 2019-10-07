using System.Collections.Generic;
using System.Linq;

namespace Bugster.Domain
{
    public class Project
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long ManagerId { get; private set; }
        public User Manager { get; private set; }

        private ICollection<User> _teamMembers;
        public IEnumerable<User> TeamMembers => _teamMembers;
        private ICollection<Bug> _bugs;
        public IEnumerable<Bug> Bugs => _bugs;

        private Project() 
        { 
        }

        public Project(string name, User manager)
        {
            Name = name;
            Description = default;
            Manager = manager;
            Manager.UpdateStatusTo(UserStatus.ACTIVE);

            _bugs = new List<Bug>();
            _teamMembers = new List<User>();
        }

        public void UpdateDescriptionTo(string description)
        {
            Description = description;
        }

        public void UpdateNameTo(string name)
        {
            Name = name;
        }

        public void UpdateManagerTo(User manager)
        {
            Manager.UpdateStatusTo(UserStatus.INACTIVE);

            Manager = manager;
            Manager.UpdateStatusTo(UserStatus.ACTIVE);
        }

        public void CreateAndAssignBug(Bug bug, long assigneeId)
        {
            if (bug == default)
            {
                return;
            }

            var foundTeamMember = _teamMembers.FirstOrDefault(user => user.Id == assigneeId);
            bug.AssignTo(foundTeamMember);

            _bugs.Add(bug);
        }
        
        public void RemoveBug(Bug bug)
        {
            if (bug == default)
            {
                return;
            }

            _bugs.Remove(bug);
        }
        
        public void AddTeamMember(User member)
        {
            if (member == default)
            {
                return;
            }

            member.UpdateStatusTo(UserStatus.ACTIVE);
            _teamMembers.Add(member);
        }
        
        public void RemoveTeamMember(User member)
        {
            if (member == default)
            {
                return;
            }

            member.UpdateStatusTo(UserStatus.INACTIVE);
            _teamMembers.Remove(member);
        }
    }
}
