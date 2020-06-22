using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Domain
{
    public class ProjectAppUser : IdentityUser
    {
        public bool Sex { get; set; }
    }
}
