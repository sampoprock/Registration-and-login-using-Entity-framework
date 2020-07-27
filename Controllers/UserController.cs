using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Registration.Models;
using System.Web.Security;

namespace Registration.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]

        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "CreatedBy, CreatedDate")] User user)
        {
        
            bool status = false;
            string message = "";
            
            if (ModelState.IsValid)
            {
                #region
                var isExist = IsEmail(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                    return View(user);
                }
                #endregion

                #region
                user.Password = Hash.HashPass(user.Password);
                user.ConfirmPassword = Hash.HashPass(user.ConfirmPassword);
                #endregion

                user.CreatedBy = "Admin";
                user.CreatedDate = Convert.ToString(DateTime.Now);

                #region Saving to databse
                using(RegistrationEntities rs=new RegistrationEntities())
                {
                    rs.Users.Add(user);
                    rs.SaveChanges();
                }
                #endregion





            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
           
        }
       

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userlogin,string ReturnUrl)
        {
            string message = "";
            using(RegistrationEntities rs=new RegistrationEntities())
            {
                var v = rs.Users.Where(a => a.Email == userlogin.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Hash.HashPass(userlogin.Password), v.Password) == 0)
                    {
                        int timeout = userlogin.RememberMe ? 525600 : 40;
                        var ticket = new FormsAuthenticationTicket(userlogin.EmailID, userlogin.RememberMe, timeout);
                        string encrypt = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid data provided";
                    }
                }
                else
                {
                    message = "Invalid data provided";
                }
            }

            ViewBag.Message = message;
            return View();
        }



        


        [NonAction]
        public bool IsEmail(string emailID)
        {
            using(RegistrationEntities rs=new RegistrationEntities())
            {
                var v = rs.Users.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }
    }


}