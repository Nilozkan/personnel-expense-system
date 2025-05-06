

namespace WebAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExpenseRequest> ExpenseRequests { get; set; } = new List<ExpenseRequest>();
    }
}