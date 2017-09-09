using ANummerGenerator.Model;
using Microsoft.AspNetCore.Mvc;

namespace ANummerGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(new ANummerModel());

        public IActionResult Error()
        {
            return View();
        }
    }
}
