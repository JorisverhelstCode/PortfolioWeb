using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Database;
using PortfolioWeb.Domain;
using PortfolioWeb.Models;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<Project> projectsFromDb = await _portfolioContext.Projects
                .Where(project => project.ProjectAppUserId == userId)
                .ToListAsync();

            List<ProjectListViewModel> projects = new List<ProjectListViewModel>();

            foreach (Project project in projectsFromDb)
            {
                projects.Add(new ProjectListViewModel() { Id = project.Id, Name = project.Name });
            }

            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Project projectsFromDb =
                await _portfolioContext.Projects
                .Include(project => project.Status)
                .Include(project => project.ProjectTags)
                .ThenInclude(projectTag => projectTag.Tag)
                .FirstOrDefaultAsync(movie => movie.Id == id && movie.ProjectAppUserId == userId);

            ProjectDetailViewModel movie = new ProjectDetailViewModel()
            {
                Name = projectsFromDb.Name,
                Description = projectsFromDb.Description,
                PhotoUrl = projectsFromDb.PhotoUrl,
                ProjectStatusID = projectsFromDb.Status.Id,
                Tags = projectsFromDb.ProjectTags.Select(projectTag => projectTag.Tag.Name)
            };

            //List<string> tags = new List<string>();

            //foreach(var movieTag in movieFromDb.MovieTags)
            //{
            //    tags.Add(movieTag.Tag.Name);
            //}

            //movie.Tags = tags;

            return View(movie);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ProjectCreateViewModel vm = new ProjectCreateViewModel();

            var projectStatuses = await _portfolioContext.ProjectStatuses.ToListAsync();

            foreach (ProjectStatus projectStatus in projectStatuses)
            {
                vm.Statuses.Add(new SelectListItem()
                {
                    Value = projectStatus.Id.ToString(),
                    Text = projectStatus.Name
                });
            }

            var tags = await _portfolioContext.Tags.ToListAsync();
            vm.Tags = tags.Select(tag => new SelectListItem() { Value = tag.Id.ToString(), Text = tag.Name }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProjectCreateViewModel createViewModel)
        {
            if (!TryValidateModel(createViewModel))
            {
                return View(createViewModel);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Project newProject = new Project()
            {
                Name = createViewModel.Name,
                Description = createViewModel.Description,
                ProjectStatusID = createViewModel.SelectedProjectStatus,
                ProjectAppUserId = userId,
                ProjectTags = createViewModel.SelectedTags.Select(tag => new ProjectTag() { TagId = tag }).ToList()
            };

            var projectTags = new List<ProjectTag>();

            foreach (var selectedTag in createViewModel.SelectedTags)
            {
                projectTags.Add(new ProjectTag() { TagId = selectedTag });
            }

            newProject.ProjectTags = projectTags;

            if (createViewModel.Photo != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(createViewModel.Photo.FileName);
                var pathName = Path.Combine(_hostEnvironment.WebRootPath, "pics");
                var fileNameWithPath = Path.Combine(pathName, uniqueFileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    createViewModel.Photo.CopyTo(stream);
                }

                newProject.PhotoUrl = "/pics/" + uniqueFileName;
            }

            _portfolioContext.Projects.Add(newProject);
            await _portfolioContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Project projectFromDb = await _portfolioContext.Projects.Include(project => project.ProjectTags).FirstOrDefaultAsync(m => m.Id == id);

            ProjectEditViewModel vm = new ProjectEditViewModel()
            {
                Name = projectFromDb.Name,
                Description = projectFromDb.Description,
                SelectedTags = projectFromDb.ProjectTags.Select(mt => mt.TagId).ToArray()
            };

            var tags = await _portfolioContext.Tags.ToListAsync();
            vm.Tags = tags.Select(tag => new SelectListItem() { Value = tag.Id.ToString(), Text = tag.Name }).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjectEditViewModel vm)
        {
            if (!TryValidateModel(vm))
            {
                return View(vm);
            }

            Project domainProject = await _portfolioContext.Projects.Include(m => m.ProjectTags).FirstOrDefaultAsync(m => m.Id == id);

            _portfolioContext.ProjectTags.RemoveRange(domainProject.ProjectTags);

            domainProject.Name = vm.Name;
            domainProject.Description = vm.Description;
            domainProject.ProjectTags = vm.SelectedTags.Select(tagId => new ProjectTag() { TagId = tagId }).ToList();

            _portfolioContext.Update(domainProject);

            await _portfolioContext.SaveChangesAsync();

            return RedirectToAction("Detail", new { Id = id });
        }

        public async Task<IActionResult> Delete(int id, string returnUrl)
        {
            Project projectFromDb = await _portfolioContext.Projects.FindAsync(id);

            return View(new ProjectDeleteViewModel() { Id = projectFromDb.Id, Naam = projectFromDb.Name });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Project movieToDelete = await _portfolioContext.Projects.FindAsync(id);
            _portfolioContext.Projects.Remove(movieToDelete);
            await _portfolioContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}