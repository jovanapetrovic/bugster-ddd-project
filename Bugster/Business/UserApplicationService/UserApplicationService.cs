using Bugster.Business.Models;
using Bugster.Domain;
using Bugster.Repositories.UserRepository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugster.Business.UserApplicationService
{
    public class UserApplicationService :
        IUserApplicationService
    {
        private readonly IUserRepository _userRepository;

        public UserApplicationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> Handle(ReadUsersRequest request)
        {
            IEnumerable<User> users = null;
            if(request.Roles != default)
            {
                var roles = request.Roles
                    .Select(role => Enum.Parse<UserRole>(role.ToUpper()));
                users = await _userRepository.ReadAll(roles);
            }
            else
            {
                users = await _userRepository.ReadAll();
            }

            var response = users.Select(user => user.Adapt<UserResponse>())
                .ToList();
            return response;
        }

        public async Task<UserResponse> Handle(ReadUserRequest request)
        {
            var user = await _userRepository.ReadById(request.UserId);
            return user.Adapt<UserResponse>();
        }

        public async Task<UserResponse> Handle(CreateUserRequest request)
        {
            if (!Enum.IsDefined(typeof(UserRole), request.Role.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("User role has wrong value");
            }

            var role = Enum.Parse<UserRole>(request.Role.ToUpper());
            var user = new User(request.Username, request.Email, request.FullName, role);

            _userRepository.Create(user, request.Password);
            await _userRepository.PersistChanges();

            return user.Adapt<UserResponse>();
        }

        public async Task<UserResponse> Handle(UpdateUserRequest request)
        {
            var user = await _userRepository.ReadById(request.UserId);
            if (user == default)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (!Enum.IsDefined(typeof(UserRole), request.Role.ToUpper()))
            {
                throw new ArgumentOutOfRangeException("User role has wrong value");
            }

            var role = Enum.Parse<UserRole>(request.Role.ToUpper());

            user.UpdateEmailTo(request.Email);
            user.UpdateFullNameTo(request.FullName);
            user.UpdateRoleTo(role);

            await _userRepository.PersistChanges();

            return user.Adapt<UserResponse>();
        }

        public async Task<UserResponse> Handle(DeleteUserRequest request)
        {
            var user = await _userRepository.ReadById(request.UserId);
            if (user == default)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _userRepository.Delete(user);
            await _userRepository.PersistChanges();

            return user.Adapt<UserResponse>();
        }
    }
}
