using System.Web.Mvc;
using ImageLoader.Models;

namespace ImageLoader.Controllers
{
    public class HomeController : Controller
    {                
        public ActionResult Index()
        {
            var userAgentEntity = new UserAgentEntity(Request.Browser.Browser + Request.Browser.Version);
            userAgentEntity.Description = Request.UserAgent;

            UserAgentLogger.LogUserAgentInfo(userAgentEntity);

            return View();
        }
    }
}
