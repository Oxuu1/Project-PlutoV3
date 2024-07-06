using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using PlutoV3Beta.Forms.Warnings;
using PlutoV3Beta.Fortnite;
using PlutoV3Beta.Properties;

namespace PlutoV3Beta.Forms
{
    public partial class PlutoHost : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
             int nLeftRect,     // x-coordinate of upper-left corner
             int nTopRect,      // y-coordinate of upper-left corner
             int nRightRect,    // x-coordinate of lower-right corner
             int nBottomRect,   // y-coordinate of lower-right corner
             int nWidthEllipse, // width of ellipse
             int nHeightEllipse // height of ellipse
        );
        public PlutoHost()
        {
            InitializeComponent();
            siticoneMaterialTextBox1.ReadOnly = true;
            siticoneMaterialTextBox1.Text = "GameServer";
            siticoneMaterialTextBox2.Text = Settings.Default.Path;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Multiselect = false;
                dialog.Title = "Please Select your FortniteGame Folder!";
                dialog.EnsureFileExists = false;
                dialog.EnsurePathExists = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var fortniteGame = dialog.FileName + "\\FortniteGame";
                    if (!Directory.Exists(fortniteGame))
                    {
                        MessageBox.Show("This Folder Does not Have FortniteGame inside of it! Make sure it does!");
                    }
                    else
                    {
                        siticoneMaterialTextBox2.Text = dialog.FileName;
                        Properties.Settings.Default.Path = dialog.FileName;
                        Properties.Settings.Default.Save();
                    }

                }
            }
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            if (siticoneMaterialTextBox1.Text == "")
            {
                MessageBox.Show("Empty Username!");
            }
            else
            {
                Properties.Settings.Default.GameServer = siticoneMaterialTextBox1.Text;
                Properties.Settings.Default.Save();
                FortniteV3.LaunchHost(siticoneMaterialTextBox2.Text, siticoneMaterialTextBox1.Text);
                GameLaunched form = new GameLaunched();
                form.Show();
            }
        }

        private void PlutoHost_Load(object sender, EventArgs e)
        {

        }

        private void siticoneCircleButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneCircleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void siticoneMaterialTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
