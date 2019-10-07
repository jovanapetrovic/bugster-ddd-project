using Bugster.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Business.ProjectApplicationService
{
    public interface ITaskAssigner
    {
        Task<long> Propose(Project project, IEnumerable<long> tags);
    }
}
