using HackatonGroup3_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TwinCAT.Ads;

namespace HackatonGroup3_Web.Controllers
{
    public class HomeController : Controller
    {
        AdsClient _client;
        HomeViewModel model;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,AdsClient adsClient, HomeViewModel o_model)
        {
            _client = adsClient;
            model = o_model;
            _logger = logger;

        }
        

        public IActionResult Index()
        {
            
            

            return View(model);
        }
        [HttpPost]
        public IActionResult Run(string ipAdress)
        {
            
            string MachineIp = string.Empty;

            try
            {
                if (String.IsNullOrEmpty(ipAdress))
                {
                    return RedirectToAction("Index");
                }
               
                MachineIp = ipAdress;
                _client.Connect(ipAdress, 851);
                model.MachineStatus = "Connected"; 

                return RedirectToAction("Index");
                
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

        }

        public IActionResult Start() 
        {
            uint motor1 = _client.CreateVariableHandle("GVL10_VAR_PRG.bTestPowerEnable12");
            uint motor2 = _client.CreateVariableHandle("GVL10_VAR_PRG.bTestPowerEnable13");
            uint motor1_2 = _client.CreateVariableHandle("GVL10_VAR_PRG.bMoveAxisMotor1_2");

            

            _client.WriteAny(motor1, true);
            _client.WriteAny(motor2, true);
            _client.WriteAny(motor1_2, true);
            model.MachineStatus = "Started";
            model.Outputs[0].State = true;
            model.Outputs[1].State = true;
            return RedirectToAction("index");
        }
        public IActionResult Stop()
        {
            uint motor1 = _client.CreateVariableHandle("GVL10_VAR_PRG.bTestPowerEnable12");
            uint motor2 = _client.CreateVariableHandle("GVL10_VAR_PRG.bTestPowerEnable13");
            uint motor1_2 = _client.CreateVariableHandle("GVL10_VAR_PRG.bMoveAxisMotor1_2");


            _client.WriteAny(motor1, false);
            _client.WriteAny(motor2, false);
            model.MachineStatus = "Connected";
            return RedirectToAction("index");
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
