﻿using CI_Entity.CIDbContext;
using CI_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class FiltersController : Controller
    {
        private readonly CiPlatformContext _db;
        public FiltersController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult _navbar()
        {
            try
            {


                List<City> citylist = _db.Cities.ToList();
                List<Country> country = _db.Countries.ToList();
                foreach (var item in citylist)
                {
                    var cityname = citylist.FirstOrDefault();
                    if (cityname != null)
                    {
                        var CityName = cityname.Name;
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
