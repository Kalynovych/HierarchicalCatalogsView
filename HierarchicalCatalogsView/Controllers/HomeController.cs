using HierarchicalCatalogsView.Models;
using Microsoft.AspNetCore.Mvc;

namespace HierarchicalCatalogsView.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("{*catchall}")]
        public IActionResult Index()
        {
            string path = Request.Path;
            ViewData["Path"] = path;
            ViewData["CurrentCatalog"] = path.Split("/")[^1].Replace("%20", " ");
            return View();
        }

        [HttpPost]
        [Route("/")]
        public IActionResult ImportCatalogsHierarchy(IFormFile file, string currentCatalog, [FromServices] DataService dataService)
        {
            if (!dataService.TryImportCatalogs(file.OpenReadStream(), currentCatalog))
            {
                ErrorViewModel error = new ErrorViewModel() { ErrorMessage = "File has an incorrect format!" };
                return View("Error", error);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet]
        [Route("/Export")]
        public IActionResult ExportCatalogsHierarchy(string catalogName, [FromServices] DataService dataService)
        {
            MemoryStream stream = dataService.ExportCatalogs(catalogName);
            if (stream == null)
            {
                ErrorViewModel error = new ErrorViewModel() { ErrorMessage = "You cannot export root folder!" };
                return View("Error", error);
            }
            return File(stream, "application/json", $"{catalogName}.json");
        }

        [HttpGet]
        [Route("/Home/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel() { ErrorMessage = "Cannot connect to the database!" });
        }
    }
}