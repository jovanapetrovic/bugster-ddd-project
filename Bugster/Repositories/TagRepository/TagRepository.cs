using Bugster.Data;
using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Repositories.TagRepository
{
    public class TagRepository :
        ITagRepository
    {
        private readonly ILogger<TagRepository> _logger;
        private readonly BugsterDbContext _context;

        public TagRepository(ILogger<TagRepository> logger,
            BugsterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Create(Tag tag)
        {
            try
            {
                _context.Tags.Add(tag);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void Delete(Tag tag)
        {
            try
            {
                _context.Tags.Remove(tag);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Tag>> ReadAll()
        {
            try
            {
                return await _context.Tags
                    .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<Tag> ReadById(long id)
        {
            try
            {
                return await _context.Tags
                    .FirstOrDefaultAsync(tag => tag.Id == id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public Task PersistChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
