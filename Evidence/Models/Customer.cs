using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int CustomerId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Vyplňte číslo dokladu nebo pasu")]
        [Display(Name = "OP/PAS")]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Vyplňte jméno")]
        [StringLength(50, ErrorMessage = "Jméno je příliš dlouhé, max. 50 znaků")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vyplňte příjmení")]
        [StringLength(50, ErrorMessage = "Příjmení je příliš dlouhé, max. 50 znaků")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "Telefon je příliš dlouhý, max. 20 znaků")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vyplňte email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email zadán v nesprávném formátu")]
        [StringLength(100, ErrorMessage = "Email je příliš dlouhý, max. 100 znaků")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte ulici")]
        [StringLength(30, ErrorMessage = "Název ulice je příliš dlouhý, max. 50 znaků")]
        [Display(Name = "Ulice")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Vyplňte číslo popisné")]
        [Range(1, int.MaxValue, ErrorMessage = "Číslo popisné musí být větší než 0")]
        [Display(Name = "Číslo popisné")]
        public string RegistryNumber { get; set; }

        [Display(Name = "Číslo orientační")]
        public string OrientationNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte město")]
        [StringLength(50, ErrorMessage = "Název města je příliš dlouhý, max. 50 znaků")]
        [Display(Name = "Město")]
        public string City { get; set; }

        [Required(ErrorMessage = "Vyplňte poštovní směrovací číslo")]
        [StringLength(10, ErrorMessage = "Poštovní směrovací číslo je příliš dlouhé, max. 10 znaků")]
        [Display(Name = "PSČ")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Vyberte stát")]
        [StringLength(50, ErrorMessage = "Název státu je příliš dlouhý, max. 50 znaků")]
        [Display(Name = "Stát")]
        public string Country { get; set; }

        public virtual ICollection<CustomerInsurance> CustomerInsurances { get; set; }
    }
}
