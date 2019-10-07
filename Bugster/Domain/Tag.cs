using System;

namespace Bugster.Domain
{
    public class Tag
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public UserRole Bound { get; private set; }

        public Tag(string name, UserRole bound)
        {
            Name = name;
            Bound = bound;
        }
        
        public Tag(long id, string name, UserRole bound)
        {
            Id = id;
            Name = name;
            Bound = bound;
        }

        public void UpdateNameTo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public void BoundToRole(UserRole role)
        {
            Bound = role;
        }
    }
}
