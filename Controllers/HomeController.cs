using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mail;

        public HomeController(IMailService mail)
        {
            _mail = mail;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactModel model)
        {
            var msg = string.Format("Comment from: {1}{0}Email:{2}{0}Comment",
                Environment.NewLine,
                model.Name,
                model.Email,
                model.Comment         
                );

            if (_mail.SendMail("noreply@topcaves.com", "bhekinkosidube@gmail.com", "Contact us Email", msg))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        [Authorize]
        public ActionResult MyMessages()
        {
            return View();
        }

    }
}