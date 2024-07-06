using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;

namespace PlutoV3Beta
{
    internal class RPC
    {

        public static DiscordRpcClient client;

        public static void Init(Label l, PictureBox pic, Label lab)
        {

            client = new DiscordRpcClient("1255132736042307685", autoEvents: true);

            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.OnReady += (sender, e) =>
            {

                var sex = l.BeginInvoke((Action)delegate { l.Text = e.User.Username; });
                var lfs = lab.BeginInvoke((Action)delegate { lab.Text = "Logged in as " + e.User.Username; });

                var egaming = pic.BeginInvoke((Action)delegate { pic.Load(e.User.GetAvatarURL(User.AvatarFormat.PNG)); });

                l.EndInvoke(sex);
                lab.EndInvoke(lfs);
                pic.EndInvoke(egaming);
            };
            client.OnError += (object sender, ErrorMessage e) =>
            {
                MessageBox.Show("Please open discord for this to work!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SystemSounds.Exclamation.Play();
                Application.Exit();
            };
            client.OnConnectionFailed += (sender, e) =>
            {
                MessageBox.Show("Please open discord for this to work!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SystemSounds.Exclamation.Play();

                Application.Exit();
            };


            client.Initialize();




            client.SetPresence(new RichPresence()
            {
                Details = "Testing V3 Beta Build",
                State = "",
                Assets = new Assets()
                {
                    LargeImageKey = "plutov3",
                    LargeImageText = ""
                }
            });

        }
        public static void Close()
        {
            client.Dispose();
        }
    }
}