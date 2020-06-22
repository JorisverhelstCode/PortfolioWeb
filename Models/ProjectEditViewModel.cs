using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Models
{
    public class ProjectEditViewModel
    {
        [DisplayName("Naam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Titel is verplicht!")]
        [MaxLength(25, ErrorMessage = "Maximum 25 karakters!")]
        public string Name { get; set; }

        [DisplayName("Omschrijving")]
        public string Description { get; set; }

        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();

        public int[] SelectedTags { get; set; }
    }
}
