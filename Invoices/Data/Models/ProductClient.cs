using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Data.Models
{
    public class ProductClient
    {
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Required]
        public Product Product { get; set; } = null!;

        [Required]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [Required]
        public Client Client { get; set; } = null!;
    }
}