
using Main.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Main.Controllers
{
    public class FormController : Controller
    {
        private readonly string root;
        public FormController()
        {
            root = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"wwwroot\"));
            
        }

        public string ReadForm(string name)
        {
            return System.IO.File.ReadAllText(root + $@"\forms\{name.ToLower()}.html");
        }

        public IActionResult GetForm(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }

            return Content(ReadForm(id), "text/plain");
        }

    }

}
