using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PortfolioWeb.Database;
using PortfolioWeb.Services;

namespace PortfolioWeb.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly PortfolioDbContext _portfolioContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPhotoService _photoService;

        public PortfolioController(PortfolioDbContext dbContext, IWebHostEnvironment environment, IPhotoService photoService)
        {
            _portfolioContext = dbContext;
            _hostEnvironment = environment;
            _photoService = photoService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}