using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

       

        [MaxLength(15)]
        public string email { get; set; }

        public string Password { get; set; }

        public ICollection<DemandeDeService> demandeDeServices { get; set; }
    }
}
