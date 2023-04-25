﻿using CI.Models;
using CI_Entity.CIDbContext;
using CI_Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Forget = CI.Models.FogetModel;
using CI.Repository.Interface;
using Newtonsoft.Json.Linq;

namespace CI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ForgetController : Controller
    {

        private readonly IUserRepository _Idb;
        public ForgetController(IUserRepository Idb)
        {

            _Idb = Idb;
        }
        public ActionResult Forget()
        {
            ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
            ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(Forget model)
        {
            try
            {
                ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
                ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);

                if (ModelState.IsValid)
                {
                    //var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);
                    var user = _Idb.UserExist(model.Email);
                    if (user == null)
                    {
                        ViewBag.emailerror = "Email not Exist";
                        return View();
                    }

                    // Generate a password reset token for the user
                    var token = Guid.NewGuid().ToString();

                    // Store the token in the password resets table with the user's email
                    _Idb.storepasswordResets(model.Email, token);


                    // Send an email with the password reset link to the user's email address
                    var resetLink = Url.Action("ResetPassword", "Forget", new { email = model.Email, token }, Request.Scheme);
                    // Send email to user with reset password link
                    // ...
                    var fromAddress = new MailAddress("communityempowermentportal@gmail.com", "Community Empowerment Portal");
                    var toAddress = new MailAddress(model.Email);
                    var subject = "Password reset request";
                    var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
                    var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("communityempowermentportal@gmail.com", "bkdkuisqaxfsjgfp"),
                        EnableSsl = true
                    };
                    smtpClient.Send(message);
                    TempData["Email Send"] = "Link for reset Password Sucessfuly Sended to registerd Email";
                    return RedirectToAction("Forget", "Forget");
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string email, string token)
        {
            try
            {
                ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
                ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);


                // var passwordReset = _db.PasswordResets.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
                var passwordReset = _Idb.PasswordResets(email, token);
                if (passwordReset == null)
                {
                    return RedirectToAction("ResetPassword", "Forget");
                }
                // Pass the email and token to the view for resetting the password
                var model = new ResetpassModel
                {
                    Email = email,
                    Token = token
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetpassModel rsp)
        {
            try
            {

                ViewBag.firstBanner = _Idb.AllBanners().Where(e => e.SortOrder == 1).ToList();
                ViewBag.Banners = _Idb.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);
                if (ModelState.IsValid)
                {
                    _Idb.Updateremovepassword(rsp.Email, rsp.Token, rsp.Password);

                    TempData["Passs change"] = "Password Change Sucessfully";
                    return RedirectToAction("Login", "Login");
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }












        // GET: ForgetController
        public ActionResult Index()

        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ForgetController/Details/5
        public ActionResult Details(int id)
        {
            try
            {


                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ForgetController/Create
        public ActionResult Create()
        {
            try
            {


                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ForgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {


                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ForgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {


                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ForgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}