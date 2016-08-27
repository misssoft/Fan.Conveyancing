using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fan.Conveyancing.Web.Controllers
{
    public class HomeController : Controller
    {
        public IConveyancingCalculator _calculator { get; set; }

        public HomeController()
        {
            _calculator = new ConveyancingCalculator(new StampDuty.StampDutyCalculator());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            var setting = new QuoteSettings(ConveyancingType.Purchase);
            return View(setting);
        }

        public ActionResult Sale()
        {
            return View();
        }

        public ActionResult Remortgage()
        {
            return View();
        }

        public ActionResult PurchaseandSale()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}