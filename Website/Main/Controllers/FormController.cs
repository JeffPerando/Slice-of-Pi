
using Main.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class FormController : Controller
    {
        private readonly ICrimeAPIService _crimeAPI;

        public FormController(ICrimeAPIService cs)
        {
            _crimeAPI = cs;
        }

        private string ReadForm(string name)
        {
            return System.IO.File.ReadAllText($"Forms/{name.ToLower()}.html");
        }

        public IActionResult GetForm(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }

            return Content(ReadForm(id), "text/html");
        }

    }

}
