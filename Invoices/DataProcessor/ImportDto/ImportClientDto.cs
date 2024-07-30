using Invoices.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    using static Invoices.Data.DataConstraints;
    [XmlType(nameof(Client))]
    public class ImportClientDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MaxLength(ClientNameMaxLength)]
        [MinLength(ClientNameMinLength)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(NumberVat))]
        [Required]
        [MaxLength(ClientNumberVatMaxLength)]
        [MinLength(ClientNumberVatMinLength)]
        public string NumberVat { get; set; } = null!;

        public ImportAddressDto[] Addresses { get; set; } = null!;


    }
}
