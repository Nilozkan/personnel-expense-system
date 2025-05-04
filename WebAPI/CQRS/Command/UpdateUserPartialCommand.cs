

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class UpdateUserPartialCommand : IRequest<BaseResponse<UserResponse>>
    {
        public int Id { get; set; }
        public UserUpdateRequest Model { get; set; }

        public UpdateUserPartialCommand(int id, UserUpdateRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}