using CI_Entity.Models;
using CI_Entity.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.Repository.Interface
{
    public interface IUserRepository
    {
        public User UserExist(string Email);
        public User Login(string Email ,string Password);
        public PasswordReset PasswordResets(string Email, string Token);
     
        public List<Country> CountryList();
        public List<City> CityList();
        public List<MissionTheme> ThemeList();    
        public List<Mission> MissionsList();
        public List<GoalMission> GoalsList();
        public List<FavoriteMission> favoriteMissions();
        public List<MissionApplication> missionApplications();
        public List<MissionRating> missionRatingList();
        public List<User> alluser();
        public MissionRating RatingExist(long Id, long missionId);
        public void ApplyMission(long missionid,long id);
        public Comment comment(long id, long missionid, string comttext);
        public bool FavMissByUserMissID(long missionid,long id);
        public List<Mission> RelatedMission(long themeid , long missionid);
        //public void Adduser(string user);
        public List<User> Adduser(User user);
        public long addstory(long MissionId, string title, DateTime date, string discription, long id,long storyid);
        public long adddraftstory(long MissionId, string title, DateTime date, string discription, long id, long storyid);
        public List<MissionMedium> MissionMediaList( );
        public List<MissionDocument> MissionDocumentList( );
        public List <PasswordReset> storepasswordResets(string Email, string Token);
        public List <PasswordReset> Updateremovepassword(string Email, string Token,string password);
        public void addstoryMedia(long MissionId, string mediatype, string mediapath, long id ,long sid);
        public void AddStoryUrl(long storyid, string url);
        public void Removemedia(long storyid);
        public List<StoryMedium> storyMedia();
        public List<Story> StoryList();
        public Comment cmtdelete(long cmtid);
        public void AddTimeMIssionTimesheetdata(long MissionId, long id, int? hour, int? minute, DateTime date, string message, int? action, long? timesheetid);

        public List<Timesheet> TimesheetList();
        public void deletedatatimesheet(long timesheetid);
        public void changepassword(string newpassword, long id);
        public List<Skill> skillList();
        public void updateuser(User user);
        public void AddUserSkills(long skillid, long userId);
        public Admin AdminbEmail(String Email);
        public MissionTheme ADDNewTheme(String Theme);
        public List<UserSkill> UserSkills(long userid);
        public List<UserSkill> allUserSkills();
        public MissionTheme AddTheme(string themeName);
        public MissionTheme UpdateTheme(string themeName, long themeId);
        public MissionTheme DeleteTheme(long themeId);
        public Skill AddSkill(string skillName);
        public Skill UpdateSkill(string skillName, long skillId);
        public Skill DeleteSkill(long skillId);
        public User AddUser(string firstname, string lastname, string email, string password, string department, string profiletext,
            string status, string employeeid, string avatar, long cityid, long countryid);
        public User UpdateUser(string firstname, string lastname, string email, string password, string department, string profiletext,
           string status, string employeeid, string avatar, long cityid, long countryid, long userId);
        public IQueryable<MissionApplicationViewModel> GetPendingMissionApplications();
        public void Approveapplication(long MaId, string status);
        public List<CmsPage> GetCmsPages();
        public void AddCms(CI_Entity.ViewModel.AdminCmsPageVM cms);
        public void UpdateCms(CI_Entity.ViewModel.AdminCmsPageVM cms);
        public void Deletecms(long id);
        public CI_Entity.ViewModel.AdminCmsPageVM GetCmsPages(long CmsPageId);
        public IQueryable<AdminStoryVM> GetPendingStories();
        public void Approvestory(long MaId, string status);
        public User UserByUserid(long userid);
        public ContactU addContactUs(string subject, string message, string username, string email);
        public Mission AddMission(AdminMissionViewModel model, IFormFileCollection? files);
        public IQueryable<SkillListVM> MissionSkilljoinSkill();
        public Mission UpdateMission(AdminMissionViewModel model, IFormFileCollection? files);
        public List<MissionMedium> allmedia();
        public void delImg(long id);
        public void delDoc(long id);
        public Banner AddBanner(string discrption, string image, int sortorder);
        public List<Banner> AllBanners();
        public Banner UpdateBanner(string discrption, string image, int sortorder, long bannerId);
        public List<UserSkill> removeUserSkills(long id);
        public void DeleteUser(long userId);
        public void DeleteMission(long missionId);
        public void DeleteStory(long storyId);
        public void DeleteBanner(long bannerId);

        public void changepass(long? id, string? password);
        public List<CmsPage> cmsdetail();
    }
}
