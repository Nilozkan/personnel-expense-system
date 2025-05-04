
using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}