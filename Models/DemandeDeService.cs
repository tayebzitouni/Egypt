using System.ComponentModel.DataAnnotations.Schema;

namespace freelanceProjectEgypt03.Models
{
    public class DemandeDeService : ClientMessage
    {
       
        public DateTime preferedDateAndTime { get; set; }

        public string location { get; set; }
        
        public decimal budget { get; set; }

        public List<string> AdditionalServices { get; set; }
        
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client client { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service service { get; set; }


    }
}
