using CI.Repository.Interface;
using CI_Entity.CIDbContext;
using CI_Entity.Models;
using CI_Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CI.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CiPlatformContext _db;

        public UserRepository(CiPlatformContext db)
        {
            _db = db;


        }
        public User UserExist(string Email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == Email);
        }
        public User Login(string Email, string Password)
        {
            return _db.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

        }
        public PasswordReset PasswordResets(string Email, string Token)
        {
            return _db.PasswordResets.FirstOrDefault(u => u.Email == Email && u.Token == Token);
        }

        public List<Country> CountryList()
        {
            return _db.Countries.ToList();
        }
        public List<City> CityList()
        {
            return _db.Cities.ToList();
        }
        public List<MissionTheme> ThemeList()
        {
            return _db.MissionThemes.ToList();
        }
        public List<Mission> MissionsList()
        {
            return _db.Missions.ToList();
        }
        public List<GoalMission> GoalsList()
        {
            return _db.GoalMissions.ToList();
        }
        public List<FavoriteMission> favoriteMissions()
        {
            return _db.FavoriteMissions.ToList();
        }
        public List<MissionApplication> missionApplications()
        {
            return _db.MissionApplications.ToList();
        }
        public List<MissionRating> missionRatingList()
        {
            return _db.MissionRatings.ToList();

        }
        public List<User> alluser()
        {
            return _db.Users.ToList();
        }
        public MissionRating RatingExist(long Id, long missionId)
        {
            return _db.MissionRatings.FirstOrDefault(u => u.MissionId == missionId && u.UserId == Id);

        }
        public void ApplyMission(long missionid, long id)
        {
            var Applied = new MissionApplication();
            {
                Applied.MissionId = missionid;
                Applied.UserId = id;
                Applied.ApprovalStatus = "0";
                Applied.AppliedAt = DateTime.Now;
            };
            _db.MissionApplications.Add(Applied);
            _db.SaveChanges();
        }
        public Comment comment(long id, long missionid, string comttext)
        {

            var newcmt = new Comment();
            {
                newcmt.UserId = id;
                newcmt.MissionId = missionid;
                newcmt.MissionTxt = comttext;

            };

            _db.Comments.Add(newcmt);

            _db.SaveChanges();
            return newcmt;
        }

        public bool FavMissByUserMissID(long missionid, long id)
        {
            return _db.FavoriteMissions.Any(u => u.UserId == id && u.MissionId == missionid);
        }
        public List<Mission> RelatedMission(long themeid, long missionid)
        {
            return _db.Missions.Where(m => m.ThemeId == themeid && m.MissionId != missionid).ToList();
        }
        public List<User> Adduser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return null;
        }

        public long addstory(long MissionId, string title, DateTime date, string discription, long id, long storyid)
        {
            if (storyid != 0)
            {
                var updatestory = _db.Stories.FirstOrDefault(x => x.StoryId == storyid);

                updatestory.MissionId = MissionId;
                updatestory.Title = title;
                updatestory.Description = discription;
                updatestory.Status = "Pending";
                updatestory.CreatedAt = date;
                updatestory.UpdatedAt= DateTime.Now;
                updatestory.UserId = id;
                _db.Update(updatestory);
                _db.SaveChanges();
                
                return updatestory.StoryId;
            }
            else
            {
                var newstory = new Story();
                newstory.MissionId = MissionId;
                newstory.Title = title;
                newstory.Description = discription;
                newstory.Status = "Pendding";
                newstory.CreatedAt = date;
                newstory.UserId = id;
                _db.Add(newstory);
                _db.SaveChanges();
                return newstory.StoryId;
            }
     
        }
        public long adddraftstory(long MissionId, string title, DateTime date, string discription, long id,long Storyid)
            
        {
            if (Storyid != 0)
            {
                 var updatedraftstory= _db.Stories.FirstOrDefault(x=>x.StoryId==Storyid);
                updatedraftstory.MissionId = MissionId;
                updatedraftstory.Title = title;
                updatedraftstory.Description = discription;
                updatedraftstory.Status = "draft";
                updatedraftstory.CreatedAt = date;
                updatedraftstory.UpdatedAt = DateTime.Now;
                updatedraftstory.UserId = id;
                _db.Update(updatedraftstory);
               _db.SaveChanges();
                return updatedraftstory.StoryId;

            }
            else
            {
                var newstory = new Story();
                newstory.MissionId = MissionId;
                newstory.Title = title;
                newstory.Description = discription;
                newstory.Status = "draft";
                newstory.CreatedAt = date;
                newstory.UserId = id;
                _db.Add(newstory);
                _db.SaveChanges();
                return newstory.StoryId;
            }
         
         
        }
        public List<MissionMedium> MissionMediaList()
        {
            return _db.MissionMedia.ToList();
        }
        public List<PasswordReset> storepasswordResets(string Email, string Token)
        {
            var passwordReset = new PasswordReset
            {
                Email = Email,
                Token = Token
            };
            _db.PasswordResets.Add(passwordReset);
            _db.SaveChanges();
            return null;
        }
        public List<PasswordReset> Updateremovepassword(string Email, string Token, string password)
        {
            // Find the user by email
            //var user = _db.Users.FirstOrDefault(u => u.Email == rsp.Email);
            var user = _db.Users.FirstOrDefault(m => m.Email == Email);



            // Find the password reset record by email and token
            //var passwordReset = _db.PasswordResets.FirstOrDefault(pr => pr.Email == rsp.Email && pr.Token == rsp.Token);
            //var passwordReset = _db.PasswordResets.FirstOrDefault(m=>m.Email==Email&& m.Token==Token );
            var passwordreset = _db.PasswordResets.FirstOrDefault(m => m.Email == Email && m.Token == Token);

            // Update the user's password

            user.Password = password;
            _db.SaveChanges();

            // Remove the password reset record from the database
            _db.PasswordResets.Remove(passwordreset);
            _db.SaveChanges();
            return null;
        }

        public void addstoryMedia(long MissionId, string mediatype, string mediapath, long id, long sid)
        {
           // var story = _db.Stories.OrderBy(s => s.CreatedAt).Where(s => s.MissionId == MissionId && s.UserId == id).FirstOrDefault();
            StoryMedium st = new StoryMedium();
            st.StoryId = sid;
            st.StoryType = mediatype;
            st.StoryPath = mediapath;
            _db.Add(st);
            _db.SaveChanges();
        }
        public void Removemedia(long storyid)
        {
            var x=_db.StoryMedia.Where(x=>x.StoryId==storyid).ToList();
            foreach(var image  in x)
            {
                _db.StoryMedia.Remove(image);
                _db.SaveChanges();
            }

        }
        public List<StoryMedium> storyMedia()
        {
            return _db.StoryMedia.ToList();
        }
        public List<Story> StoryList()
        {
            return _db.Stories.ToList();
        }
        
        public Comment cmtdelete(long cmtid)
        {
            var commtdetail=_db.Comments.FirstOrDefault(x=>x.CommentId==cmtid);
            _db.Comments.Remove(commtdetail);
            _db.SaveChanges();
            return null;
            
        }
        public void AddTimeMIssionTimesheetdata(long MissionId, long id, int? hour, int? minute, DateTime date, string message, int? action, long? timesheetid)
        {
            if (timesheetid == 0)
            {
                if (hour != null && minute != null)
                {
                    var timesheet = new Timesheet();
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.TimesheetTime = hour + ":" + minute;
                    timesheet.DateVolunteered = date;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.CreatedAt = DateTime.Now;
                    _db.Add(timesheet);
                    _db.SaveChanges();
                }
                else
                {
                    var timesheet = new Timesheet();
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.DateVolunteered = date;
                    timesheet.Action = action;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.CreatedAt = DateTime.Now;
                    _db.Add(timesheet);
                    _db.SaveChanges();
                }
            }
            else
            {
                if (hour != null && minute != null)
                {
                    var timesheet =  _db.Timesheets.FirstOrDefault(t => t.TimesheetId == timesheetid);
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.TimesheetTime = hour + ":" + minute;
                    timesheet.DateVolunteered = date;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.UpdatedAt = DateTime.Now;
                    _db.Update(timesheet);
                    _db.SaveChanges();
                }
                else
                {
                    var timesheet = _db.Timesheets.FirstOrDefault(t => t.TimesheetId == timesheetid);
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.DateVolunteered = date;
                    timesheet.Action = action;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.UpdatedAt = DateTime.Now;
                    _db.Update(timesheet);
                    _db.SaveChanges();
                }
            }








        }
        public List<Timesheet> TimesheetList()
        {
            return _db.Timesheets.Where(x=>x.DeletedAt==null).ToList();
        }
        public void deletedatatimesheet( long timesheetid)
        {
            var row =_db.Timesheets.FirstOrDefault(x=> x.TimesheetId==timesheetid);
            row.DeletedAt= DateTime.Now;
            row.Status = "3";
            _db.Timesheets.Update(row);
            _db.SaveChanges();
        }

        public void changepassword(string newpassword, long id)
        {
            var user=_db.Users.FirstOrDefault(x=>x.UserId==id);
            if(user!=null)
            {
            user.Password=newpassword;
                user.UpdatedAt= DateTime.Now;
                _db.Users.Update(user);
                _db.SaveChanges();

            }
        }
        public List<Skill> skillList()
        {
            return _db.Skills.ToList();
        }
        public void updateuser(User user)
        {
            _db.Update(user);
            _db.SaveChanges();
        }
        public void AddUserSkills(long skillid, long userId)
        {
            var userskills = new UserSkill();
            userskills.UserId = userId;
            userskills.SkillId = skillid;
            _db.Add(userskills);
            _db.SaveChanges();
        }
        public Admin AdminbEmail(String Email)
        {
            return _db.Admins.Where(u => u.Email == Email).FirstOrDefault();
        }
        public MissionTheme ADDNewTheme(String Theme)
        {
            var theme = new MissionTheme();
            theme.Title = Theme;
            _db.Add(theme);
            _db.SaveChanges();
            return theme;
        }

        /*sbghdfhgfdjgfdjg*/


        
        public List<UserSkill> UserSkills(long userid)
        {
            return _db.UserSkills.ToList();
        }

        public ContactU addContactUs(string subject, string message, string username, string email)
        {
            var contactUs = new ContactU();
            contactUs.UserName = username;
            contactUs.Email = email;
            contactUs.Subject = subject;
            contactUs.Message = message;

            _db.Add(contactUs);
            _db.SaveChanges();
            return contactUs;
        }
        public MissionTheme AddTheme(string themeName)
        {
            var missiontheme = new MissionTheme();
            missiontheme.Title = themeName;
            missiontheme.CreatedAt = DateTime.Now;
            _db.Add(missiontheme);
            _db.SaveChanges();
            return missiontheme;
        }
        public MissionTheme UpdateTheme(string themeName, long themeId)
        {
            var missiontheme = _db.MissionThemes.FirstOrDefault(t => t.MissionThemeId == themeId);
            missiontheme.Title = themeName;
            missiontheme.UpdatedAt = DateTime.Now;
            _db.Update(missiontheme);
            _db.SaveChanges();
            return missiontheme;
        }
        public MissionTheme DeleteTheme(long themeId)
        {
            var theme = _db.MissionThemes.FirstOrDefault(t => t.MissionThemeId == themeId);
            theme.DeletedAt = DateTime.Now;
            theme.Status = 0;
            _db.Update(theme);
            _db.SaveChanges();
            return theme;
        }
        public Skill AddSkill(string skillName)
        {
            var skill = new Skill();
            skill.SkillName = skillName;
            skill.CreatedAt = DateTime.Now;
            _db.Add(skill);
            _db.SaveChanges();
            return skill;
        }
        public Skill UpdateSkill(string skillName, long skillId)
        {
            var skills = _db.Skills.FirstOrDefault(t => t.SkillId == skillId);
            skills.SkillName = skillName;
            skills.UpdatedAt = DateTime.Now;
            _db.Update(skills);
            _db.SaveChanges();
            return skills;
        }
        public Skill DeleteSkill(long skillId)
        {
            var skill = _db.Skills.FirstOrDefault(t => t.SkillId == skillId);
            skill.DeletedAt = DateTime.Now;
            skill.Status = "0";
            _db.Update(skill);
            _db.SaveChanges();
            return skill;
        }
        public User AddUser(string firstname, string lastname, string email, string password, string department, string profiletext,
            string status, string employeeid, string avatar, long cityid, long countryid)
        {
            var userexist = _db.Users.Where(e => e.Email == email).Any();
            if (!userexist)
            {
                var user = new User();
                user.FirstName = firstname;
                user.LastName = lastname;
                user.Email = email;
                user.Password = password;
                user.Department = department;
                user.Status = status;
                user.EmployeeId = employeeid;
                user.Avatar = avatar;
                user.CityId = cityid;
                user.CountryId = countryid;
                user.ProfileText = profiletext;
                _db.Add(user);
                _db.SaveChanges();
                return user;
            }
            else
            {
                return _db.Users.Find(email);
            }

        }
        public User UpdateUser(string firstname, string lastname, string email, string password, string department, string profiletext,
    string status, string employeeid, string avatar, long cityid, long countryid, long userId)
        {

            var user = _db.Users.FirstOrDefault(e => e.UserId == userId);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            user.Password = password;
            user.Department = department;
            user.Status = status;
            user.EmployeeId = employeeid;
            user.Avatar = avatar;
            user.CityId = cityid;
            user.CountryId = countryid;
            user.ProfileText = profiletext;
            user.UpdatedAt = DateTime.Now;
            _db.Update(user);
            _db.SaveChanges();
            return user;

        }
        public IQueryable<MissionApplicationViewModel> GetPendingMissionApplications()
        {
            var applicationsList = from ma in _db.MissionApplications
                                   join m in _db.Missions on ma.MissionId equals m.MissionId
                                   join u in _db.Users on ma.UserId equals u.UserId
                                   where ma.ApprovalStatus == "0"
                                   select new MissionApplicationViewModel
                                   {
                                       UserId = u.UserId,
                                       MissionId = ma.MissionId,
                                       Title = m.Title,
                                       AppliedAt = ma.AppliedAt,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       MissionApplicationId = ma.MissionApplicationId,
                                   };

            return applicationsList;
        }

        public void Approveapplication(long MaId, string status)
        {
            var ma = _db.MissionApplications.FirstOrDefault(e => e.MissionApplicationId == MaId);
            ma.ApprovalStatus = status;
            _db.Update(ma);
            _db.SaveChanges();
        }

        public List<CmsPage> GetCmsPages()
        {
            return _db.CmsPages.Where(e => e.DeletedAt == null).ToList();
        }

        public void AddCms(CI_Entity.ViewModel.AdminCmsPageVM cms)
        {
            var cmsPage = new CmsPage();
            cmsPage.Title = cms.Title;
            cmsPage.Description = HttpUtility.HtmlEncode(cms.Description);
            cmsPage.Status = cms.Status;
            cmsPage.Slug = cms.Slug;
            _db.Add(cmsPage);
            _db.SaveChanges();
        }

        public void UpdateCms(CI_Entity.ViewModel.AdminCmsPageVM cms)
        {
            var cmsPage = _db.CmsPages.FirstOrDefault(e => e.CmsPageId == cms.CmsPageId);
            cmsPage.Title = cms.Title;
            cmsPage.Description = HttpUtility.HtmlEncode(cms.Description);
            cmsPage.Status = cms.Status;
            cmsPage.Slug = cms.Slug;
            cmsPage.UpdatedAt = DateTime.Now;
            _db.Update(cmsPage);
            _db.SaveChanges();
        }

        public void Deletecms(long id)
        {
            var cmsPage = _db.CmsPages.FirstOrDefault(e => e.CmsPageId == id);
            cmsPage.DeletedAt = DateTime.Now;
            _db.Update(cmsPage);
            _db.SaveChanges();
        }

        public CI_Entity.ViewModel.AdminCmsPageVM GetCmsPages(long CmsPageId)
        {
            var cms = _db.CmsPages.FirstOrDefault(e => e.CmsPageId == CmsPageId);
            var cmsPage = new CI_Entity.ViewModel.AdminCmsPageVM();
            cmsPage.CmsPageId = CmsPageId;
            cmsPage.CmsPages = _db.CmsPages.Where(e => e.DeletedAt == null).ToList();
            cmsPage.Title = cms.Title;
            cmsPage.Description = HttpUtility.HtmlDecode(cms.Description);
            cmsPage.Status = cms.Status;
            cmsPage.Slug = cms.Slug;

            return cmsPage;
        }

        public IQueryable<AdminStoryVM> GetPendingStories()
        {
            var applicationsList = from ma in _db.Stories
                                   join m in _db.Missions on ma.MissionId equals m.MissionId
                                   join u in _db.Users on ma.UserId equals u.UserId
                                   where ma.Status == "Pending"
                                   select new AdminStoryVM
                                   {
                                       StoryId = ma.StoryId,
                                       MissionTitle = m.Title,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       StoryTitle = ma.Title,
                                   };
            return applicationsList;
        }

        public void Approvestory(long MaId, string status)
        {
            var ma = _db.Stories.FirstOrDefault(e => e.StoryId == MaId);
            ma.Status = status;
            _db.Update(ma);
            _db.SaveChanges();
        }
        public User UserByUserid(long userid)
        {
            return _db.Users.FirstOrDefault(u => u.UserId == userid);
        }
    }
}   
