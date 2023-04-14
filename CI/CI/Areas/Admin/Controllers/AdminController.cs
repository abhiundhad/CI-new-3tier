using CI.Models;
using CI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _Idb;
        public AdminController(IUserRepository Idb)
        {

            _Idb = Idb;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminCms()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminMission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminTheme()
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var themevm = new AdminPanaltehmeViewModal();
            themevm.MissionThemes = _Idb.ThemeList();
            return View(themevm);
        }
        [HttpPost]
        public IActionResult AdminTheme(AdminPanaltehmeViewModal theme)
        {
            _Idb.ADDNewTheme(theme.NewTheme);
            var themevm = new AdminPanaltehmeViewModal();
            themevm.MissionThemes = _Idb.ThemeList();
            return View(themevm);
        }
        public IActionResult AdminSkills()
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminMissionApplication()
        {
            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }

        public IActionResult AdminStory()
        {
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }



        public IActionResult AdminBannerManagement()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }


    }
}
