using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence.Models
{
    public class CustomerIndexData
    {
        IEnumerable<Customer> Customers { get; set; }
        IEnumerable<Insurance> Insurances { get; set; }
    }
}
