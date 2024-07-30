using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace Invoices.Data.Models
{
    using static DataConstraints;
    using Enums;
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        public ICollection<ProductClient> ProductsClients { get; set; }
            = new HashSet<ProductClient>();


    }
}
