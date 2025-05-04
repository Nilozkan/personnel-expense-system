

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class UpdateUserCommand : IRequest<BaseResponse<UserResponse>>
    {
        public int Id { get; set; }
        public UserRequest Model { get; set; }

        public UpdateUserCommand(int id, UserRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}