using CI_Entity.Models;
using CI.Repository.Interface;
using CI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using CI_Entity.ViewModel;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IUserRepository _Idb;
        public AdminController(IUserRepository Idb, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _Idb = Idb;

            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {

            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var user = new CI_Entity.ViewModel.AdminUserViewModel();
            user.users = _Idb.alluser().ToList();
            user.allcity = _Idb.CityList();
            user.allcountry = _Idb.CountryList();
            return View(user);

        }
        [HttpPost]
        public async Task<IActionResult> AddUser(string firstname, string lastname, string email, string password, string profiletext, string department,
            string status, string employeeid, string avatar, long userId, long cityid, long countryid)
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if (userId == null || userId == 0)
            {

                var savedUser = _Idb.AddUser(firstname, lastname, email, password, department, profiletext, status, employeeid, avatar, cityid, countryid);
                if (savedUser.Email == email)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {

                var savedUser = _Idb.UpdateUser(firstname, lastname, email, password, department, profiletext, status, employeeid, avatar, cityid, countryid, userId);
                return RedirectToAction("Index");
            }

            var uservm = new CI_Entity.ViewModel.AdminUserViewModel();
            uservm.users = _Idb.alluser().ToList();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetUser(long userId)
        {
            var user = _Idb.UserByUserid(userId);
            var userVM = new CI_Entity.ViewModel.AdminUserViewModel();
            userVM.users = _Idb.alluser().ToList();
            userVM.allcity = _Idb.CityList();
            userVM.allcountry = _Idb.CountryList();
            userVM.firstname = user.FirstName;
            userVM.lastname = user.LastName;
            userVM.email = user.Email;
            userVM.avatar = user.Avatar;
            userVM.status = user.Status;
            userVM.cityid = user.CityId;
            userVM.employeeid = user.EmployeeId;
            userVM.countryid = user.CountryId;
            userVM.profiletext = user.ProfileText;
            userVM.department = user.Department;
            return View("Index", userVM);
        }
        public IActionResult AdminCms()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var cms = new CI_Entity.ViewModel.AdminCmsPageVM();
            cms.CmsPages = _Idb.GetCmsPages();
            return View(cms);
        }

        [HttpPost]
        public IActionResult AddCms(CI_Entity.ViewModel.AdminCmsPageVM model)
        {
            if (model.CmsPageId == 0 || model.CmsPageId == null)
            {
                _Idb.AddCms(model);

            }
            else
            {
                _Idb.UpdateCms(model);
            }
            return RedirectToAction("AdminCms");
        }


        [HttpPost]
        public IActionResult GetCms(long CmsPageId)
        {
            var page = _Idb.GetCmsPages(CmsPageId);
            return View("AdminCms", page);
        }

        [HttpPost]
        public IActionResult DeleteCms(long CmsPageId)
        {
            _Idb.Deletecms(CmsPageId);
            return RedirectToAction("AdminCms");
        }



        public IActionResult AdminMission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var missionvm = new AdminMissionViewModel();
            missionvm.missions = _Idb.MissionsList();
            missionvm.countries = _Idb.CountryList();
            missionvm.cities = _Idb.CityList();
            missionvm.themes = _Idb.ThemeList();
            missionvm.AllSkills = _Idb.skillList();

            //var skills = _Idb.MissionSkilljoinSkill();
            //var uskills = skills.Where(e => e.MissionId == ).ToList();
            //ViewBag.userskills = uskills;
            //foreach (var skill in uskills)
            //{
            //    var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
            //    allskills.Remove(rskill);
            //}
            //ViewBag.remainingSkills = allskills;
            return View(missionvm);
        }

        [HttpPost]
        public IActionResult AddMission(AdminMissionViewModel model)
        {
            if (model.missionId == null || model.missionId == 0)
            {
                var files = Request.Form.Files;
                _Idb.AddMission(model, files);

            }
            else
            {
                var files = Request.Form.Files;
                var savedUser = _Idb.UpdateMission(model, files);
                return RedirectToAction("AdminMission");
            }

            var missionvm = new AdminMissionViewModel();
            missionvm.missions = _Idb.MissionsList();
            missionvm.countries = _Idb.CountryList();
            missionvm.cities = _Idb.CityList();
            missionvm.themes = _Idb.ThemeList();

            return RedirectToAction("AdminMission");
        }
        [HttpPost]
        public IActionResult GetMission(long missionId)
        {
            var mission = _Idb.MissionsList().FirstOrDefault(t => t.MissionId == missionId && t.Status == "1" && t.DeletedAt == null);
            var goalmission = _Idb.GoalsList().FirstOrDefault(g => g.MissionId == missionId);
            var allskills = _Idb.skillList();
            var skillsJoin = _Idb.MissionSkilljoinSkill();
            var missionskill = skillsJoin.Where(m => m.MissionId == missionId).ToList();
            foreach (var skill in missionskill)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            var finalurl = "";
            var VideoURLs = _Idb.allmedia().Where(e => e.MissionId == missionId && e.MediaType == "Video").ToList();
            foreach (var videoURL in VideoURLs)
            {
                finalurl = finalurl + videoURL.MediaPath + "\r\n";
            }
            var missionVm = new AdminMissionViewModel();
            missionVm.missions = _Idb.MissionsList().Where(t => t.Status == "1" && t.DeletedAt == null).ToList();
            missionVm.title = mission.Title;
            mission.MissionId = mission.MissionId;
            missionVm.editor2 = mission.Description;
            missionVm.shortdescription = mission.ShortDescription;
            missionVm.startDate = mission.StartDate;
            missionVm.endDate = mission.EndDate;
            missionVm.deadline = mission.Deadline;
            missionVm.cityId = mission.CityId;
            missionVm.countryId = mission.CountryId;
            missionVm.themeId = mission.ThemeId;
            missionVm.timeavailability = mission.AvailabilityTime;
            missionVm.organizationDetail = mission.OrganizationDetail;
            missionVm.organizationName = mission.OrganizationName;
            missionVm.totalseats = mission.Availability;
            missionVm.goalValue = goalmission.GoalValue;
            missionVm.goalObjectiveText = goalmission.GoalObjectiveText;
            missionVm.missionType = mission.MissionType;
            missionVm.countries = _Idb.CountryList();
            missionVm.cities = _Idb.CityList();
            missionVm.themes = _Idb.ThemeList();
            missionVm.AllSkills = _Idb.skillList();
            missionVm.MissionSkill = missionskill;
            missionVm.RemainingSkills = allskills;
            missionVm.url = finalurl;
            missionVm.ImageFiles = new List<MissionMedium>();
            missionVm.DocFiles = new List<IFormFile>();
            var imgfiles = _Idb.MissionMediaList().Where(m => m.MissionId == missionId && m.MediaType != "Video" && m.DeletedAt == null).ToList();
            var docfiles = _Idb.MissionDocumentList().Where(m => m.MissionId == missionId  && m.DeletedAt == null).ToList();
            int i = 1;
            if (imgfiles.Count > 0)
            {
                foreach (var ifile in imgfiles)
                {
                    missionVm.ImageFiles.Add(ifile);
                }
            }
            i = 0;
            foreach (var ifile in docfiles)
            {
                i++;
                string fileName = "example" + i + "." + ifile.DocumentType;  // specify the file name and extension
                string contentType = ifile.MissionDocumentId.ToString();
                MemoryStream ms = new MemoryStream(ifile.DocumentPath);
                var file = new FormFile(ms, 0, ms.Length, contentType, fileName);
                missionVm.DocFiles.Add(file);
            }

            //Request.Form.Files= missionVm.ImageFiles;
            return View("AdminMission", missionVm);
        }

        [HttpPost]
        public IActionResult delDoc(string docId)
        {
            _Idb.delDoc(long.Parse(docId));
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult delImg(long imgId)
        {
            _Idb.delImg(imgId);
            return Json(new { success = true });
        }
        public IActionResult AdminTheme()
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _Idb.ThemeList().Where(t => t.Status == 1).ToList();
            return View(themevm);
        }
        [HttpPost]
        public IActionResult AddTheme(AdminThemeViewModel model)
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if (model.themeId == null || model.themeId == 0)
            {
                _Idb.AddTheme(model.themeName);
            }
            else
            {
                _Idb.UpdateTheme(model.themeName, model.themeId);
            }

            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _Idb.ThemeList().Where(t => t.Status == 1).ToList();
            themevm.themeName = "";
            return RedirectToAction("AdminTheme");
        }

        [HttpPost]
        public IActionResult GetTheme(long themeId)
        {
            var theme = _Idb.ThemeList().FirstOrDefault(t => t.MissionThemeId == themeId && t.Status == 1);
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _Idb.ThemeList().Where(t => t.Status == 1).ToList();
            themevm.themeName = theme.Title;
            themevm.themeId = theme.MissionThemeId;
            return View("AdminTheme", themevm);
        }
        [HttpPost]
        public IActionResult deleteTheme(long themeId)
        {
            _Idb.DeleteTheme(themeId);
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _Idb.ThemeList().Where(t => t.Status == 1).ToList();
            themevm.themeName = "";
            return RedirectToAction("AdminTheme");
        }

        public IActionResult AdminSkills()
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var skill = new AdminSkillViewModel();
            skill.skill = _Idb.skillList().Where(t => t.Status == "1").ToList();

            return View(skill);

        }

        [HttpPost]
        public IActionResult AddSkill(AdminSkillViewModel model)
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if (model.skillId == null || model.skillId == 0)
            {
                _Idb.AddSkill(model.skillName);
            }
            else
            {
                _Idb.UpdateSkill(model.skillName, model.skillId);
            }

            var skill = new AdminSkillViewModel();
            skill.skill = _Idb.skillList().Where(t => t.Status == "1").ToList();
            skill.skillName = "";
            return RedirectToAction("AdminSkills");

        }
        [HttpPost]
        public IActionResult GetSkill(long skillId)
        {
            var skilllist = _Idb.skillList().FirstOrDefault(t => t.SkillId == skillId && t.Status == "1");
            var skill = new AdminSkillViewModel();
            skill.skill = _Idb.skillList().Where(t => t.Status == "1").ToList();
            skill.skillName = skilllist.SkillName;
            skill.skillId = skilllist.SkillId;
            return View("AdminSkills", skill);
        }
        [HttpPost]
        public IActionResult DeleteSkill(long skillId)
        {
            _Idb.DeleteSkill(skillId);
            var skillvm = new AdminSkillViewModel();
            skillvm.skill = _Idb.skillList().Where(t => t.Status == "1").ToList();
            skillvm.skillName = "";
            return RedirectToAction("AdminSkills");
        }
        public IActionResult AdminMissionApplication()
        {
            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var applicationsList = _Idb.GetPendingMissionApplications().ToList();
            return View(applicationsList);
        }




        [HttpPost]
        public IActionResult ApproveApplication(long MaId, string status)
        {
            _Idb.Approveapplication(MaId, status);
            return RedirectToAction("AdminMissionApplication");
        }

        public IActionResult AdminStory()
        {
            var userId = HttpContext.Session.GetString("userID");

            ViewBag.UserId = int.Parse(userId);
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var strories = _Idb.GetPendingStories().ToList();
            return View(strories);
        }

        [HttpPost]
        public IActionResult ApproveStory(long MaId, string status)
        {
            _Idb.Approvestory(MaId, status);
            return RedirectToAction("AdminStory");
        }






        public IActionResult AdminBannerManagement()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }




    }
}