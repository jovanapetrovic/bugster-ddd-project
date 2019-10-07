using Bugster.Data;
using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Repositories.ProjectRepository
{
    public class ProjectRepository :
        IProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;
        private readonly BugsterDbContext _context;

        public ProjectRepository(ILogger<ProjectRepository> logger,
            BugsterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Project> ReadById(long id)
        {
            try
            {
                return await _context.Projects
                    .Include(project => project.Bugs)
                    .Include(project => project.Manager)
                    .Include(project => project.TeamMembers)
                    .FirstOrDefaultAsync(project => project.Id == id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Project>> ReadAll()
        {
            try
            {
                return await _context.Projects
                    .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void Create(Project project)
        {
            try
            {
                _context.Projects.Add(project);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void Delete(Project project)
        {
            try
            {
                _context.Projects.Remove(project);
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
