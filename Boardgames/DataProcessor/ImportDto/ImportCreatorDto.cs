using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    using static Boardgames.Data.DataConstraints;
    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [XmlElement(nameof(FirstName))]
        [MaxLength(CreatorFirstNameMaxLength)]
        [MinLength(CreatorFirstNameMinLength)]
        [Required]
        public string FirstName { get; set; } = null!;

        [XmlElement(nameof(LastName))]
        [MaxLength(CreatorLastNameMaxLength)]
        [MinLength(CreatorLastNameMinLength)]
        [Required]
        public string LastName { get; set; } = null!;

        public BoardgameDto[] Boardgames { get; set; } = null!;
    }
}
