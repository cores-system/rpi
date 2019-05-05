using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Rpi.Controllers;
using Rpi.Engine;

namespace Rpi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Timer timer = new Timer(new TimerCallback(Cron), null, 0, 1000 * 60);
            CreateWebHostBuilder(args).Build().Run();
            timer.Dispose();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(op => op.Listen(IPAddress.Any, 60817));


        public static void Cron(object ob)
        {
            if (AmixerController.NextTimeMute != default)
            {
                if (DateTime.Now > AmixerController.NextTimeMute)
                {
                    AmixerController.NextTimeMute = default;
                    Bash.Run($"amixer set 'PCM' mute", out _);
                }
            }
            else
            {
                // Ночной режим
                if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour < 9)
                {
                    // Кто-то включил звук
                    if (AmixerController.MinutesTimeToMute != 0 && Bash.Invoke("amixer | grep \"Mono: \"").Contains("[on]"))
                        AmixerController.NextTimeMute = DateTime.Now.AddMinutes(AmixerController.MinutesTimeToMute);
                }
                else
                {
                    // Дневной режим
                    AmixerController.MinutesTimeToMute = 0;
                }
            }
        }
    }
}
