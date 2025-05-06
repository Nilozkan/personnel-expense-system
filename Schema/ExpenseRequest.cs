

namespace Schema
{
    public class ExpenseRequestRequest
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
    }

    public class ExpenseRequestResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
    }

    public class ApproveExpenseRequest
    {
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
    }
}