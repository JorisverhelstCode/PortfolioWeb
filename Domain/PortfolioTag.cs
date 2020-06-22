using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class PortfolioTag
    {
        public int PortfolioID { get; set; }
        public Portfolio Portfolio { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
