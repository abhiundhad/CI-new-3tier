using CI.Models;
using CI.Repository.Interface;
using CI_Entity.CIDbContext;
using CI_Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _Idb;

        public LoginController(IUserRepository Idb)
        {


            _Idb = Idb;
        }
        public IActionResult Login(long missionid, int StoryId)
        {
            try
            {
                ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
                ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);

                HttpContext.Session.Clear();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult Logout()
        {
            try
            {


                HttpContext.Session.Clear();
                return RedirectToAction("Landingpage", "Landingpage");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            try
            {
                ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
                ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);

                if (model.MissionId != 0 && model.MissionId != null)
                {
                  
                    // var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                    var user = _Idb.Login(model.Email, model.Password);
                    if (user != null)
                    {
                        HttpContext.Session.SetString("userID", user.UserId.ToString());
                        HttpContext.Session.SetString("Firstname", user.FirstName);
                        HttpContext.Session.SetString("useravtar", user.Avatar);
                        return RedirectToAction("Volunteering", "Volunteering", new { @id = user.UserId, missionid = model.MissionId });

                    }
                    else
                    {
                        return View();
                    }

                }
                else if (model.StoryId != null)
                {
                 
                    //var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                    var user = _Idb.Login(model.Email, model.Password);
                   
                   if (user != null)
                    {
                        HttpContext.Session.SetString("userID", user.UserId.ToString());
                        HttpContext.Session.SetString("Firstname", user.FirstName);
                        if (user.Avatar != null)
                        {
                            HttpContext.Session.SetString("useravtar", user.Avatar);
                        }

                        return RedirectToAction("StoryDetail", "StoryListing", new { @id = user.UserId, model.StoryId });

                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {


                        var admin = _Idb.AdminbEmail(model.Email);
                        //var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                        var user = _Idb.Login(model.Email, model.Password);
                        var username = model.Email.Split("@")[0];
                        if (admin != null)
                        {
                            if (admin.Password == model.Password)
                            {
                                HttpContext.Session.SetString("userID", admin.AdminId.ToString());
                                HttpContext.Session.SetString("Firstname", admin.FirstName);
                               
                                return RedirectToAction("index", "Admin", new { area = "Admin" });
                            }
                            else
                            {
                                ViewBag.Email = "email or pass is incorrect";
                            }
                        }
                        else
                       if (user != null)
                        {
                            HttpContext.Session.SetString("userID", user.UserId.ToString());
                            HttpContext.Session.SetString("Firstname", user.FirstName);
                            if (user.Avatar != null)
                            {
                                HttpContext.Session.SetString("useravtar", user.Avatar);
                            }
                            TempData["Done"] = "logged in ";
                            return RedirectToAction("Landingpage", "Landingpage", new { @id = user.UserId });
                        }
                        else
                        {
                            ViewBag.Error = "Email or Password is Incorrect";

                        }
                    }
                    return View();

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        //    [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction(nameof(LoginController.Login), "Login");
        //}
    }


}
