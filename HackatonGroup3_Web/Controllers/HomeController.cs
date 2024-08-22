using HackatonGroup3_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TwinCAT.Ads;

namespace HackatonGroup3_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
      
            return View();
        }

        public IActionResult Run()
        {
            AdsClient client = new AdsClient();

            try
            {
                client.Connect("127.0.0.0", 851);
                
                if(client.IsConnected)
                {
                    uint motor = client.CreateVariableHandle("varname");
                    client.WriteAny(motor, true);
                }
            }
            catch (Exception)
            {

                throw new Exception("Error for connection");
            }

            // un Lac contient un ARC = lac droit, RC = lac courber, popup= lac vire
            // lac 02 RC 
            // Pour chac Rc / ARC on a des capteur = sont des entre
            // les controlleur sont des sorties
            // Scanneur permet de determiner la direction soit sur lac 01 ou lac 02

            // Lac01(id, ARC, RC, ARC09(Popup)) // PopUp tourne si l'avant derniere 3iem value est 8.
            //Lac02(id, RC)
            // ARC(id, capteurData, next(controller, motor)) // captuerdata sera envoyer a nextcontroller de prochaine RC
            // RC(id, capteurData, next(controller, motor))

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
