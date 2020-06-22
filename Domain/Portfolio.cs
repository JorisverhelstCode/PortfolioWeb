using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class Portfolio
    {
        public string Title { get; set; }

        [Key]
        public int Id { get; set; }

        public ICollection<PortfolioTag> PortfolioTags { get; set; }
    }
}
