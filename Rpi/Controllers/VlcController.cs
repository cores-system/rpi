using System;
using Microsoft.AspNetCore.Mvc;
using Rpi.Engine;

namespace Rpi.Controllers
{
    public class VlcController : ControllerBase
    {
        [Route("vlc/play")]
        public ActionResult<string> Play(string uri)
        {
            Bash.Signal();
            if (!Bash.Run($"killall vlc; export DISPLAY=:0; vlc --fullscreen {uri}", out string exception))
                return exception;

            return uri;
        }
    }
}
