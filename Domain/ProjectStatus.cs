using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class ProjectStatus
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
