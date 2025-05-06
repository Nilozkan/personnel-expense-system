

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetAllExpenseRequestsQuery : IRequest<BaseResponse<List<ExpenseRequestResponse>>>
    {
        public int? UserId { get; set; }
         public GetAllExpenseRequestsQuery(int? userId = null) => UserId = userId;

    }
}