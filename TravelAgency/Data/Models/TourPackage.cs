namespace TravelAgency.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstraits;
    public class TourPackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PackageNameMaxLength)]
        public string PackageName { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}