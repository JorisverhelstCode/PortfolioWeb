﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class Project
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [Key]
        public int Id { get; set; }

        public ICollection<ProjectTag> ProjectTags { get; set; }
        public string PhotoUrl { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public int ProjectStatusID { get; set; }
        public ProjectAppUser ProjectAppUser { get; set; }
        public string ProjectAppUserId { get; set; }
    }
}
