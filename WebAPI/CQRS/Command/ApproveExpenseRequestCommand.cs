

using Base;
using MediatR;

namespace WebAPI.CQRS.Command
{
    public class ApproveExpenseRequestCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
        
        public ApproveExpenseRequestCommand(int id, bool isApproved, string? rejectionReason)
        {
            Id = id;
            IsApproved = isApproved;
            RejectionReason = rejectionReason;
        }
    }
}