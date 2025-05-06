

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS
{
    public class UpdateExpenseRequestCommand : IRequest<BaseResponse<ExpenseRequestResponse>>
    {
        public int Id { get; set; }
        public ExpenseRequestRequest Model { get; set; }
        
        public UpdateExpenseRequestCommand(int id, ExpenseRequestRequest model)
        {
            Id = id;
            Model = model;
        }
    }
}