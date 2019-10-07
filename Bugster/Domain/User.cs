using System;

namespace Bugster.Domain
{
    public class User
    {
        public long Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public UserRole Role { get; private set; }
        public UserStatus Status { get; private set; }

        private User()
        {
        }

        public User(string username, string email, string fullName, UserRole role)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            Role = role;
            Status = UserStatus.INACTIVE;
        }
        
        public void UpdateEmailTo(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            Email = email;
        }
        
        public void UpdateFullNameTo(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            FullName = fullName;
        }
        
        public void UpdateRoleTo(UserRole role)
        {
            Role = role;
        }

        public void UpdateStatusTo(UserStatus status)
        {
            Status = status;
        }
    }
}
