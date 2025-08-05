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

            public DateTime StartDate { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

        }

        public class CreateClientDto
        {
            [Required]
            public string Name { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class ContactUsDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

            public Priorite Priorite { get; set; }

            public ContactMethode PreferedContactMethode { get; set; }

            public string Description { get; set; }

            public DateTime Messagedate { get; set; }

            public string Subject { get; set; }
        }

        public class PartnerDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Location { get; set; }

            public string Description { get; set; }

            public DateTime StartDate { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

            public int ServiceId { get; set; }
        }

        public class CreatePartnerDto
        {
            public string Name { get; set; }

            public string Location { get; set; }

            public string Description { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class ServiceDto
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public int DurationMin { get; set; }

            public int DurationMax { get; set; }

            public DurationUnit DurationUnit { get; set; }

            public List<string> Details { get; set; } = new();

            public decimal Price { get; set; }

           
        }

        public class CreateServiceDto
        {
            [Required]
            public string Title { get; set; }

            public string Description { get; set; }

            public int DurationMin { get; set; }

            public int DurationMax { get; set; }

            public DurationUnit DurationUnit { get; set; }

            public List<string> Details { get; set; } = new();

            public decimal Price { get; set; }

            public int PartnerId { get; set; }

            public List<IFormFile> Files { get; set; } = new();
        }

        public class FileAttachmentDto
        {
            public int Id { get; set; }

            public string FileName { get; set; }

            public string FilePath { get; set; }

            public long Size { get; set; }

            public DateTime UploadedAt { get; set; }
        }

        public class DemandeDeServiceDto
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public Priorite Priorite { get; set; }
            public ContactMethode PreferedContactMethode { get; set; }
            public string Description { get; set; }

            public DateTime PreferedDateAndTime { get; set; }
            public string Location { get; set; }
            public decimal Budget { get; set; }
            public List<string> AdditionalServices { get; set; }

            public int ClientId { get; set; }
            public int ServiceId { get; set; }
            public string ClientName { get; set; }
            public string ServiceTitle { get; set; }
        }

    }

}
