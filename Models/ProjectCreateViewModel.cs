using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioWeb.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Models
{
    public class ProjectCreateViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public int Id { get; set; }

        public List<SelectListItem> Tags { get; set; }
        public IFormFile Photo { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public int SelectedProjectStatus { get; set; }
        public ProjectAppUser ProjectAppUser { get; set; }
        public string ProjectAppUserId { get; set; }
        public int[] SelectedTags { get; set; }
    }
}
