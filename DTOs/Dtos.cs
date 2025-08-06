namespace freelanceProjectEgypt03.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using global::freelanceProjectEgypt03.Models;
    using Microsoft.AspNetCore.Http;

    namespace freelanceProjectEgypt03.Dtos
    {
        public class ClientDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public DateTime StartDate { get; set; }
        }




        public class CreateClientDto
        {
            [Required]
            [MinLength(3)]
            public string Name { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(6)]
            public string Password { get; set; }

            [Required]
            public DateTime StartDate { get; set; }
        }



public class ContactUsDto
        {
            public int id { get; set; }
            
            [Required(ErrorMessage = "Le nom est requis.")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 100 caractères.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "L'email est requis.")]
            [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
            [RegularExpression(@"^\d{8,15}$", ErrorMessage = "Le numéro de téléphone doit contenir uniquement des chiffres (8 à 15 chiffres).")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Le sujet est requis.")]
            [StringLength(200, MinimumLength = 3, ErrorMessage = "Le sujet doit contenir entre 3 et 200 caractères.")]
            public string Subject { get; set; }

            [Required(ErrorMessage = "La description est requise.")]
            [StringLength(1000, MinimumLength = 10, ErrorMessage = "La description doit contenir entre 10 et 1000 caractères.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "La priorité est requise.")]
            [RegularExpression(@"^(normal|urgent|medium)$", ErrorMessage = "La priorité doit être 'normal', 'urgent' ou 'medium'.")]
            public string Priorite { get; set; }

            [Required]
            [RegularExpression(@"^(gmail|phone|whatsapp)$", ErrorMessage = "Le mode de contact doit être 'gmail', 'phone' ou 'whatsapp'.")]
            public string PreferedContactMethode { get; set; }

            public DateTime Date { get; set; } = DateTime.Now;
        }


        public class ContactUsResponseDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

            public string Subject { get; set; }

            public string Description { get; set; }

            public string Priorite { get; set; }

            public string PreferedContactMethode { get; set; }

            public DateTime Date { get; set; }
        }

        public class CreatePartnerDto
        {
            public int id { get; set; }
            [Required]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 100 caractères.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "La localisation est requise.")]
            public string Location { get; set; }

            [Required(ErrorMessage = "La description est requise.")]
            public string Description { get; set; }

            public DateTime StartDate { get; set; }

            [StringLength(15, ErrorMessage = "Le numéro de téléphone est invalide.")]
            [RegularExpression(@"^\d{8,15}$", ErrorMessage = "Le numéro de téléphone doit contenir uniquement des chiffres.")]
            public string PhoneNumber { get; set; }

            [StringLength(50)]
            [EmailAddress(ErrorMessage = "Adresse email invalide.")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 4)]
            public string Password { get; set; }

            [Required(ErrorMessage = "ServiceId requis.")]
            public int ServiceId { get; set; }
        }

        public class PartnerDto
        {
           public int id { get; set; }
            public string Name { get; set; }

            public string Location { get; set; }


            public DateTime StartDate { get; set; }
            public string Description { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }


            public int serviceId { get; set; }
        }

        public class ServiceDto
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Title { get; set; }

            [MaxLength(1000)]
            public string Description { get; set; }

            [Range(1, 1000)]
            public int DurationMin { get; set; }

            [Range(1, 1000)]
            public int DurationMax { get; set; }

            [Required]
            [RegularExpression("^(Hour|Day|Week)$", ErrorMessage = "DurationUnit must be Hour, Day, or Week.")]
            public string DurationUnit { get; set; }

            public List<string> Details { get; set; } = new();

            [Range(0.0, double.MaxValue)]
            public decimal Price { get; set; }

            public List<IFormFile> Files { get; set; } = new();
        }

        public class GetServiceDto
        {
            
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public int DurationMin { get; set; }
                public int DurationMax { get; set; }
                public string DurationUnit { get; set; }
                public decimal Price { get; set; }
                public List<string> details { get; set; }

                public List<FileAttachmentDto> Files { get; set; }
            
        }

        public class FileAttachmentDto
        {
            public int Id { get; set; }

            public string FileName { get; set; }

            public string FilePath { get; set; }

            public long Size { get; set; }

            public DateTime UploadedAt { get; set; }

            public string FileUrl { get; set; }
        }

        //public class DemandeDeServiceDto
        //{
        //    public int Id { get; set; }
        //    public DateTime Date { get; set; }
        //    public string Priorite { get; set; }
        //    public string PreferedContactMethode { get; set; }
        //    public string Description { get; set; }

        //    public DateTime PreferedDateAndTime { get; set; }
        //    public string Location { get; set; }
        //    public decimal Budget { get; set; }
        //    public List<string> AdditionalServices { get; set; }

        //    public int ClientId { get; set; }
        //    public int ServiceId { get; set; }
        //    public string ClientName { get; set; }
        //    public string ServiceTitle { get; set; }
        //}

        public class CreateDemandeDeServiceDto
        {
            [Required]
            public DateTime PreferedDateAndTime { get; set; }

            [Required, StringLength(255)]
            public string Location { get; set; }

            [Range(0, double.MaxValue)]
            public decimal Budget { get; set; }

            [Required]
            public List<string> AdditionalServices { get; set; }

            [Required]
            public int ClientId { get; set; }

            [Required]
            public int ServiceId { get; set; }

            [Required]
            public DateTime Date { get; set; }

            [Required(ErrorMessage = "Priorite is required.")]
            [RegularExpression("^(normal|medium|urgent)$", ErrorMessage = "Priorite must be one of: normal, medium, urgent.")]
            public string Priorite { get; set; }

            [Required(ErrorMessage = "PreferedContactMethode is required.")]
            [RegularExpression("^(gmail|phone|whatsapp)$", ErrorMessage = "PreferedContactMethode must be one of: gmail, phone, whatsapp.")]
            public string PreferedContactMethode { get; set; }


            [Required]
            public string Description { get; set; }
        }

        public class ReadDemandeDeServiceDto
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Priorite { get; set; }
            public string PreferedContactMethode { get; set; }
            public string Description { get; set; }

            public DateTime PreferedDateAndTime { get; set; }
            public string Location { get; set; }
            public decimal Budget { get; set; }
            public List<string> AdditionalServices { get; set; }

            public int ClientId { get; set; }
            public string ClientName { get; set; }

            public int ServiceId { get; set; }
            public string ServiceName { get; set; }
        }

        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class TokenDto
        {
            public string Token { get; set; }
        }
    }

}
