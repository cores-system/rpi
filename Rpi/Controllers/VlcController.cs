using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Rpi.Controllers
{
    public class VlcController : ControllerBase
    {
        [Route("vlc/play")]
        public ActionResult<string> Play(string uri)
        {
            try
            {
                var processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.FileName = "/bin/bash";
                processInfo.Arguments = $" -c \"killall vlc; export DISPLAY=:0; vlc --fullscreen {uri}\"";
                var process = Process.Start(processInfo);

                return uri;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
