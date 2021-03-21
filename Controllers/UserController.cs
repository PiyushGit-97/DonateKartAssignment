using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Assignment.Controllers
{
    public class UserController : Controller
    {
        IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Createuser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Createuser(UserDetails userDetails)
        {
            try
            {
                string filePath = _config["DonorFilePath"] + userDetails.UserName + ".txt";// _config["Filepath"];
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Name : " + userDetails.UserName + ", Email ID : " + userDetails.Email + ", Phone Number : " + userDetails.PhoneNo);
                }

                ViewBag.status = "success";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.errorMsg = "Error : " + ex.Message;
                return View();
            }
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(EmailDto emailDetails)
        {
            try
            {
                string filePath = _config["MailerFilePath"] + emailDetails.FromEmail + ".txt";// _config["Filepath"];
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("To : " + emailDetails.ToEmail);
                    writer.WriteLine("From : " + emailDetails.FromEmail);
                    writer.WriteLine("Subject : " + emailDetails.Subject);
                    writer.WriteLine("Message : " + emailDetails.Body);
                }

                ViewBag.status = "success";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.errorMsg = "Error : " + ex.Message;
                return View();
            }
        }
    }
}
