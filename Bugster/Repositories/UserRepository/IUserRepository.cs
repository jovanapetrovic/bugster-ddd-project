using Bugster.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> ReadById(long id);
        Task<IEnumerable<User>> ReadAll();
        Task<IEnumerable<User>> ReadAll(IEnumerable<UserRole> roles);

        void Create(User user, string password);
        void Delete(User user);

        Task PersistChanges();
    }
}
