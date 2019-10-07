using Bugster.Business.Models;
using Bugster.Domain;
using Bugster.Repositories.TagRepository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Business.TagsApplicationService
{
    public class TagApplicationService :
        ITagApplicationService
    {
        private readonly ITagRepository _tagRepository;

        public TagApplicationService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagResponse>> Handle(ReadTagsRequest request)
        {
            var tags = await _tagRepository.ReadAll();

            var response = tags.Select(tag => tag.Adapt<TagResponse>())
                .ToList();
            return response;
        }

        public async Task<TagResponse> Handle(ReadTagRequest request)
        {
            var tag = await _tagRepository.ReadById(request.TagId);
            return tag.Adapt<TagResponse>();
        }

        public async Task<TagResponse> Handle(CreateTagRequest request)
        {
            if (!Enum.IsDefined(typeof(UserRole), request.Bound.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("User role has wrong value");
            }

            var tagBoundToRole = Enum.Parse<UserRole>(request.Bound.ToUpper());
            var tag = new Tag(request.Name, tagBoundToRole);

            _tagRepository.Create(tag);
            await _tagRepository.PersistChanges();

            return tag.Adapt<TagResponse>();
        }

        public async Task<TagResponse> Handle(UpdateTagRequest request)
        {
            var tag = await _tagRepository.ReadById(request.TagId);
            if (tag == default)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            if (!Enum.IsDefined(typeof(UserRole), request.Bound.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("User role has wrong value");
            }

            var role = Enum.Parse<UserRole>(request.Bound.ToUpper());
            tag.UpdateNameTo(request.Name);
            tag.BoundToRole(role);

            await _tagRepository.PersistChanges();

            return tag.Adapt<TagResponse>();
        }

        public async Task<TagResponse> Handle(DeleteTagRequest request)
        {
            var tag = await _tagRepository.ReadById(request.TagId);
            if (tag == default)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _tagRepository.Delete(tag);
            await _tagRepository.PersistChanges();

            return tag.Adapt<TagResponse>();
        }
    }
}
