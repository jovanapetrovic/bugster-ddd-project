using Bugster.Data;
using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Repositories.UserRepository
{
    public class UserRepository :
        IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly BugsterDbContext _context;

        public UserRepository(ILogger<UserRepository> logger,
            BugsterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Create(User user, string password)
        {
            try
            {
                _context.Users.Add(user);
                var userEntry = _context.Entry(user);
                userEntry.Property<string>("Password").CurrentValue = password;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void Delete(User user)
        {
            try
            {
                _context.Users.Remove(user);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAll()
        {
            try
            {
                return await _context.Users
                    .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAll(IEnumerable<UserRole> roles)
        {
            try
            {
                return await _context.Users
                    .Where(user => roles.Contains(user.Role) && user.Status == UserStatus.INACTIVE)
                    .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<User> ReadById(long id)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(user => user.Id == id);
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
