using Boardgames.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    
    using static Boardgames.Data.DataConstraints;
    [XmlType(nameof(Creator))]
    public class ExportCreatorDto
    {
        [XmlAttribute(nameof(BoardGamesCount))]
        public int BoardGamesCount { get; set; }

        [XmlElement(nameof(Name))]
        [MaxLength(CreatorFirstNameMaxLength)]
        [MinLength(CreatorFirstNameMinLength)]
        [Required]
        public string Name { get; set; } = null!;

        public Boardgame[] Boardgames { get; set; }
    }
}
