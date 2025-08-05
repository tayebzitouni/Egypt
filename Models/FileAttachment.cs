using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace freelanceProjectEgypt03.Models
{
    public class FileAttachment
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FilePath { get; set; } // Or URL if stored externally

        public long Size { get; set; }

        public DateTime UploadedAt { get; set; }

        // Foreign Key
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}
