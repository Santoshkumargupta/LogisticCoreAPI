using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Domain.Model
{
    [Table("Supplier")]
    public class Supplier :IDEntity
    {
        public string SupplierName { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
         public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
