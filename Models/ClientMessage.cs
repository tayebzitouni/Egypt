using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Models
{
    public class ClientMessage 
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
       

        public string priorite { get; set; }

        public string PreferedContactMethode { get; set; }

        public string description { get; set; }
    }
}
