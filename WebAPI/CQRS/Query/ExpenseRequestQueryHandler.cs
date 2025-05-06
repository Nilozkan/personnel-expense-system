

using Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.DbContext;

namespace WebAPI.CQRS.Query
{
    public class ExpenseRequestQueryHandler :
    IRequestHandler<GetAllExpenseRequestsQuery, BaseResponse<List<ExpenseRequestResponse>>>,
    IRequestHandler<GetExpenseRequestByIdQuery, BaseResponse<ExpenseRequestResponse>>
    {
         private readonly AppDbContext _context;

        public ExpenseRequestQueryHandler(AppDbContext context) => _context = context;
        public async Task<BaseResponse<List<ExpenseRequestResponse>>> Handle(GetAllExpenseRequestsQuery request, CancellationToken cancellationToken)
        {
             var query = _context.ExpenseRequests
            .Include(e => e.Category)
            .Include(e => e.User)
            .AsQueryable();

            if (request.UserId.HasValue)
                query = query.Where(e => e.UserId == request.UserId.Value);

            var expenses = await query
                .Select(e => new ExpenseRequestResponse
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Status = e.Status.ToString(),
                    RejectionReason = e.RejectionReason,
                    CreatedAt = e.CreatedAt,
                    CategoryName = e.Category.Name,
                    UserName = $"{e.User.Name} {e.User.Surname}"
                }).ToListAsync(cancellationToken);

            return new BaseResponse<List<ExpenseRequestResponse>>(expenses, "Expenses listed");
        }

        public async Task<BaseResponse<ExpenseRequestResponse>> Handle(GetExpenseRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _context.ExpenseRequests
            .Include(e => e.Category)
            .Include(e => e.User)
            .Where(e => e.Id == request.Id)
            .Select(e => new ExpenseRequestResponse
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                Status = e.Status.ToString(),
                RejectionReason = e.RejectionReason,
                CreatedAt = e.CreatedAt,
                CategoryName = e.Category.Name,
                UserName = $"{e.User.Name} {e.User.Surname}"
            }).FirstOrDefaultAsync(cancellationToken);

            if (expense == null)
                return new BaseResponse<ExpenseRequestResponse>("Expense not found");

            return new BaseResponse<ExpenseRequestResponse>(expense, "Expense retrieved");
        }
    }
}