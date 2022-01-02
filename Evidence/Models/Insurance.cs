using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence.Models
{
    public class Insurance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "Vyplňte název pojištění")]
        [StringLength(100, ErrorMessage = "Název je příliš dlouhý, max. 100 znaků")]
        [Display(Name = "Název pojištění")]
        public string NameInsurance { get; set; }

        [Required(ErrorMessage = "Vyplňte popis pojištění")]
        [StringLength(500, ErrorMessage = "Popis je příliš dlouhý, max. 500 znaků")]
        [Display(Name = "Popis pojištění")]
        public string Description { get; set; }

        public virtual ICollection<CustomerInsurance> CustomerInsurances { get; set; }
    }
}
