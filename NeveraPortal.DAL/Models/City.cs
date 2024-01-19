using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
