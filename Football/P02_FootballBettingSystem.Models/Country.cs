using P02_FootballBetting.Common;
using P02_FootballBetting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBettingSystem.Models
{
    public class Country
    {

        public Country()
        {
            Towns = new List<Town>();
        }

        [Key]
        public int CountryId { get; set; }

        [MaxLength(ValidationConstants.CountryMaxLength )]
        public string Name { get; set; }


        public virtual  ICollection<Town> Towns{ get; set; }

    }
}
