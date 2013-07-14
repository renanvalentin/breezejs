using Breeze.WebApi;
using NetBiis.Data;
using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetBiis.Controllers
{
    public class MainController : Controller
    {
        readonly UserRepository _userRepository = new UserRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            _userRepository.Save(user);

            return View();
        }
    }
}
