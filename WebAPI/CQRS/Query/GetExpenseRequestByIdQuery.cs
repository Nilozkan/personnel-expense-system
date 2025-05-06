

using Base;
using MediatR;
using Schema;

namespace WebAPI.CQRS.Query
{
    public class GetExpenseRequestByIdQuery : IRequest<BaseResponse<ExpenseRequestResponse>>
    {
        public int Id { get; set; }
        public GetExpenseRequestByIdQuery(int id) => Id = id;
    }
}