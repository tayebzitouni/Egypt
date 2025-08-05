using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int DurationMin { get; set; }  // Minimum duration
        public int DurationMax { get; set; }  // Maximum duration

        public DurationUnit DurationUnit { get; set; }  // Enum (Hour, Day, etc.)


        public List<string> details { get; set; } = new();

        public decimal Price { get; set; }

        public ICollection<FileAttachment> Files { get; set; }

        public ICollection<Partner> Partners { get; set; }

        public ICollection<DemandeDeService> demandeDeServices { get; set; }


    }
}
