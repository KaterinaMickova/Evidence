using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence.Models
{
    public class CustomerInsurance
    {
        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
        public int InsuranceId { get; set; }    
        public virtual Insurance Insurances { get; set; }      
    }
}
