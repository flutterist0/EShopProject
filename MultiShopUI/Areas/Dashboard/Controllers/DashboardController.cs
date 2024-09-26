using Microsoft.AspNetCore.Mvc;

namespace EShopUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
