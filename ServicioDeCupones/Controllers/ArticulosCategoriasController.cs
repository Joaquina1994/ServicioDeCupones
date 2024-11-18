using Microsoft.AspNetCore.Mvc;

namespace ServicioDeCupones.Controllers
{
    public class ArticulosCategoriasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
