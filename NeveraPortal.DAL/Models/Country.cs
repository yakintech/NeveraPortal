using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public ICollection<City> Cities { get; set; }
    }
}
