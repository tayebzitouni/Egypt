using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace freelanceProjectEgypt03.Models
{
    public class ContactUs : ClientMessage 
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }



        public string Phone { get; set; }
        public string Subject { get; set; }
    }
}
