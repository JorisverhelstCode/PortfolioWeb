﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PortfolioTag> PortfolioTags { get; set; }
    }
}
