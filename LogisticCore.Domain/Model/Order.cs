using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Domain.Model
{
    [Table("Order")]
    public class Order :IDEntity
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
         public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
