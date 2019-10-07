using Bugster.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Repositories.TagRepository
{
    public interface ITagRepository
    {
        Task<Tag> ReadById(long id);
        Task<IEnumerable<Tag>> ReadAll();

        void Create(Tag tag);
        void Delete(Tag tag);

        Task PersistChanges();
    }
}
