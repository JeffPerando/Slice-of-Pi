
using Main.DAL.Abstract;
using Main.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Main.Controllers
{
    public class FormController : Controller
    {
        public string ReadForm(string name)
        {
            return FileHelper.ReadStr($@"\forms\{name.ToLower()}.html");
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
