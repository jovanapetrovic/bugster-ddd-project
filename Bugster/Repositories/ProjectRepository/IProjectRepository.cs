using Bugster.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Repositories.ProjectRepository
{
    public interface IProjectRepository
    {
        Task<Project> ReadById(long id);
        Task<IEnumerable<Project>> ReadAll();

        void Create(Project project);
        void Delete(Project project);

        Task PersistChanges();
    }
}
