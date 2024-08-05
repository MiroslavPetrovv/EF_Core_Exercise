using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos
{
    using static TravelAgency.Data.DataConstraits;

    [XmlType(nameof(Customer))]

    public class ImportCustomerDto
    {
        [XmlElement("FullName")]
        [Required]
        [MaxLength(CustomerFullNameMaxLength)]
        [MinLength(CustomerFullNameMinLength)]
        public string FullName { get; set; } = null!;

        [XmlElement("Email")]
        [Required]
        [MaxLength(CustomerEmailMaxLength)]
        [MinLength(CustomerEmailMinLength)]
        public string Email { get; set; } = null!;

        [XmlAttribute("phoneNumber")]
        [Required]
        [RegularExpression(CustomerPhoneNumberPattern)]
        public string PhoneNumber { get; set; } = null!;


    }
}
