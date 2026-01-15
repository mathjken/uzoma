using System.ComponentModel.DataAnnotations.Schema;

namespace Uzoma.Api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Relationships
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        // Snapshot values
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
