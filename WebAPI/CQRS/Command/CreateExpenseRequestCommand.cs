

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Command
{
    public class CreateExpenseRequestCommand : IRequest<BaseResponse<ExpenseRequestResponse>>
    {
    public ExpenseRequestRequest Model { get; set; }
    public int UserId { get; set; }
    
    public CreateExpenseRequestCommand(ExpenseRequestRequest model, int userId)
    {
        Model = model;
        UserId = userId;
    }
    }
}