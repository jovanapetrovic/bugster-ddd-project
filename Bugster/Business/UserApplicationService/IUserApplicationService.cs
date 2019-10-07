using Bugster.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugster.Business.UserApplicationService
{
    public interface IUserApplicationService
    {
        Task<IEnumerable<UserResponse>> Handle(ReadUsersRequest request);
        Task<UserResponse> Handle(ReadUserRequest request);
        Task<UserResponse> Handle(CreateUserRequest request);
        Task<UserResponse> Handle(UpdateUserRequest request);
        Task<UserResponse> Handle(DeleteUserRequest request);
    }
}
