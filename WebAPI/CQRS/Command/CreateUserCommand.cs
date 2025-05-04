

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class CreateUserCommand : IRequest<BaseResponse<UserResponse>>
    {
        public UserRequest Model { get; set; }

        public CreateUserCommand(UserRequest model)
        {
            Model = model;
        }
        
    }
}