

using Base;
using MediatR;

namespace WebAPI.CQRS.Command
{
    public class DeleteExpenseRequestCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
         public DeleteExpenseRequestCommand(int id) => Id = id;
    }
}