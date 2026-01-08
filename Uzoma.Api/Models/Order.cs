using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uzoma.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Who placed the order
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Order metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Pending"; 
        // Pending | Paid | Shipped | Completed | Cancelled

        // Navigation
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        // Computed total (calculated server-side)
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
    }
}
