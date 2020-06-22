using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class Project
    {
        public string Title { get; set; }

        [Key]
        public int Id { get; set; }

        public ICollection<ProjectTag> ProjectTags { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
