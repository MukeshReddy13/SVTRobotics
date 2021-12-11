using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVTRobotics.BusinessLogic;
using SVTRobotics.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SVTRobotics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobertLocatorController : ControllerBase
    {
        public RobertLocatorController()
        {
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("GetToken")]
        public async Task<OutputValues> GetToken(InputValues inputValues)
        {
            OutputValues jsonRootClass = new OutputValues();
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OutputValues>>(new WebClient().DownloadString("https://60c8ed887dafc90017ffbd56.mockapi.io/robots"));
            list.ForEach(refVal => CalculateDistance.UpdateDistance(refVal, refVal.robotId, refVal.batteryLevel, refVal.x, refVal.y, inputValues));
            var takeTop5 = list.OrderBy(x => x.distance).Take(5).ToList();
            var outputValues = CalculateDistance.BestDistanceWithBattery(takeTop5);

            return outputValues;
        }
    }
}
