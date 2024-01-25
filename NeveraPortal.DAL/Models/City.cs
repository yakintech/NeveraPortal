using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
		[Required(ErrorMessage = "Please select a country")]
		public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
