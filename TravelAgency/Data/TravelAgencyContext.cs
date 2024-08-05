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
        public decimal Price { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; } = new List<TourPackageGuide>();
    }
}
