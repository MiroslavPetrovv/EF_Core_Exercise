using P02_FootballBetting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Models
{
    public class Player
    {
        public Player()
        {
            PlayersStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int PlayerId { get; set; }

        [MaxLength(ValidationConstants.PlayerMaxLenght)]
        public string Name { get; set; } = null!;

        public int SquadNumber { get; set; }

        public bool IsInjured { get; set; }




        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        public Position Position { get; set; }


        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [ForeignKey(nameof(TownId))]
        public int TownId { get; set; }

        public Town Town { get; set; }

        

        public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }
    }
}
