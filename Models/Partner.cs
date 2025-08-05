using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Models
{
    public class Partner
    {

        [Key] 
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

       

        public DateTime StartDate { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }


        [MaxLength(15)]
        public string email { get; set; }

        public string Password { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service service { get; set; }

    }





}

