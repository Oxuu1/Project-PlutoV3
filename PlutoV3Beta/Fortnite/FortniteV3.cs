using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlutoV3Beta.Fortnite
{
    internal class FortniteV3
    {
        public static void LaunchHost(string path, string username)
        {
            string shipping = "/FortniteGame/Binaries/Win64/FortniteClient-Win64-Shipping.exe";
            string args = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -AUTH_LOGIN=" + username + "@fortnite.com -AUTH_PASSWORD=FortniteLauncher -AUTH_TYPE=epic";
            Process FN = new Process();
            string fullpath = path + shipping;
            FN.StartInfo.Arguments = args;
            FN.StartInfo.FileName = fullpath;
            FN.StartInfo.UseShellExecute = false;
            FN.StartInfo.RedirectStandardOutput = true;
            FN.Start();
            int id = FN.Id;
            string Backend = Directory.GetCurrentDirectory() + "\\PlutoV3.dll";
            PlutoInjector.Inject(id, Backend);
            for (; ; )
            {
                string Output = FN.StandardOutput.ReadLine();
                bool init = Output.Contains("Game Engine Initialized");
                bool flag = init;
                if (flag)
                {
                    string console = Directory.GetCurrentDirectory() + "\\Framework.dll";
                    Thread.Sleep(3000);
                    PlutoInjector.Inject(id, console);
                    string GameServer = Directory.GetCurrentDirectory() + "\\GameServer.dll";
                    Thread.Sleep(9000);
                    MessageBox.Show("Click OK When in Lobby");
                    PlutoInjector.Inject(id, GameServer);
                }
            }
        }
    }
}
