using Boardgames.Data.Models;
using Newtonsoft.Json;
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
    
    public class ImportSellerDto
    {
       
        [Required]
        [MaxLength(SellerNameMaxLength)]
        [MinLength(SellerNameMinLength)]
        public string Name { get; set; } = null!;

        
        [Required]
        [MaxLength(SellerAddressMaxLength)]
        [MinLength(SellerAddressMinLength)]

        public string Address { get; set; } = null!;

        
        [Required]
        public string Country { get; set; } = null!;

        
        [Required]
        [RegularExpression(@"www\.[a-zA-z0-9-]+\.com")]
        public string Website { get; set; } = null!;

        [JsonProperty("Boardgames")]public  int[] Boardgames { get; set; } = null!;
            
    }
}
