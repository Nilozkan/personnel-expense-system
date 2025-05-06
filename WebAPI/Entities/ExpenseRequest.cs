

namespace WebAPI.Entities
{
    public enum ExpenseStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        public string? RejectionReason { get; set; } // Red sebebi
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}