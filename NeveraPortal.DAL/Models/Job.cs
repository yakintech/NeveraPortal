using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.DAL.Models
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public bool Status { get; set; } // 0 - open, 1 - closed

        public int? CityId { get; set; }
        public City City { get; set; }

        public string Keywords { get; set; }

        //parttime, fulltime, freelance, internship
        public string JobType { get; set; }

        //remote, office
        public string JobLocation { get; set; }


    }
}
