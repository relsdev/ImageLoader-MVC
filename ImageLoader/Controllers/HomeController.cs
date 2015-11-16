using ImageLoader.Data;
using ImageLoader.Domain;
using System.Linq;
using System.Web.Mvc;

namespace ImageLoader.Controllers
{
    public class HomeController : Controller
    {                
        public ActionResult Index()
        {            
            return View();
        }
    }
}
