

using Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schema;
using WebAPI.DbContext;
using WebAPI.Entities;

namespace WebAPI.CQRS.Command
{
    public class ExpenseRequestCommandHandler :
    IRequestHandler<CreateExpenseRequestCommand, BaseResponse<ExpenseRequestResponse>>,
    IRequestHandler<UpdateExpenseRequestCommand, BaseResponse<ExpenseRequestResponse>>,
    IRequestHandler<DeleteExpenseRequestCommand, BaseResponse<bool>>,
    IRequestHandler<ApproveExpenseRequestCommand, BaseResponse<bool>>
    {
        private readonly AppDbContext _context;

        public ExpenseRequestCommandHandler(AppDbContext context) => _context = context;
        public async Task<BaseResponse<ExpenseRequestResponse>> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var expense = new ExpenseRequest
        {
            Description = request.Model.Description,
            Amount = request.Model.Amount,
            CategoryId = request.Model.CategoryId,
            UserId = request.UserId,
            Status = ExpenseStatus.Pending
        };

        _context.ExpenseRequests.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        var response = await GetExpenseResponse(expense.Id);
        return new BaseResponse<ExpenseRequestResponse>(response, "Expense request created");
        }

        public async Task<BaseResponse<ExpenseRequestResponse>> Handle(UpdateExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var expense = await _context.ExpenseRequests.FindAsync(request.Id);
            if (expense == null)
                return new BaseResponse<ExpenseRequestResponse>("Expense not found");

            expense.Description = request.Model.Description;
            expense.Amount = request.Model.Amount;
            expense.CategoryId = request.Model.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
            var response = await GetExpenseResponse(expense.Id);
            return new BaseResponse<ExpenseRequestResponse>(response, "Expense updated");
        }

        public async Task<BaseResponse<bool>> Handle(DeleteExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var expense = await _context.ExpenseRequests.FindAsync(request.Id);
            if (expense == null)
                return new BaseResponse<bool>("Expense not found");

            _context.ExpenseRequests.Remove(expense);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<bool>(true, "Expense deleted");
        }

        public async Task<BaseResponse<bool>> Handle(ApproveExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var expense = await _context.ExpenseRequests.FindAsync(request.Id);
        if (expense == null)
            return new BaseResponse<bool>("Expense not found");

        expense.Status = request.IsApproved ? ExpenseStatus.Approved : ExpenseStatus.Rejected;
        expense.RejectionReason = request.RejectionReason;

        await _context.SaveChangesAsync(cancellationToken);
        return new BaseResponse<bool>(true, $"Expense {(request.IsApproved ? "approved" : "rejected")}");
    }

    private async Task<ExpenseRequestResponse> GetExpenseResponse(int id)
    {
        return await _context.ExpenseRequests
            .Include(e => e.Category)
            .Include(e => e.User)
            .Where(e => e.Id == id)
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
            }).FirstOrDefaultAsync();
    
        }
    }
}