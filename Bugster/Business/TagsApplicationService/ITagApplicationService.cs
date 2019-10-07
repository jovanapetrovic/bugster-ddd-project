using Bugster.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Business.TagsApplicationService
{
    public interface ITagApplicationService
    {
        Task<IEnumerable<TagResponse>> Handle(ReadTagsRequest request);
        Task<TagResponse> Handle(ReadTagRequest request);
        Task<TagResponse> Handle(CreateTagRequest request);
        Task<TagResponse> Handle(UpdateTagRequest request);
        Task<TagResponse> Handle(DeleteTagRequest request);
    }
}
