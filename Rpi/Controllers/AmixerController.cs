using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Rpi.Engine;

namespace Rpi.Controllers
{
    public class AmixerController : ControllerBase
    {
        public static DateTime NextTimeMute = default;
        public static int MinutesTimeToMute = 0;


        [Route("amixer/timermute")]
        public ActionResult<string> TimerMute(int minutes = 120)
        {
            MinutesTimeToMute = minutes;
            NextTimeMute = DateTime.Now.AddMinutes(minutes);
            Bash.Signal();
            return NextTimeMute.ToString();
        }

        [Route("amixer/deletetasks")]
        public ActionResult<bool> DeleteTasks()
        {
            Bash.Signal();
            NextTimeMute = default;
            MinutesTimeToMute = 0;
            return true;
        }
    }
}
