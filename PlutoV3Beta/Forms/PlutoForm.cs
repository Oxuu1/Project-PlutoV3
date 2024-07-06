using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using ns1;
using PlutoV3Beta.Forms.Warnings;
using PlutoV3Beta.Properties;

namespace PlutoV3Beta
{
    public partial class PlutoForm : Form
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
        public PlutoForm()
        {
            InitializeComponent();
            UsernameTexbox.Text = Settings.Default.Username;
            PathTextBox.Text = Settings.Default.Path;
            if (Process.GetProcessesByName("Discord").Length > 0)
            {
                RPC.Init(label2, siticoneCirclePictureBox1, label3);
            }
            else
            {
                MessageBox.Show("Could not find discord! Make sure it is open!");
            }

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        private void PlutoForm_Load(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            if (UsernameTexbox.Text == "")
            {
                MessageBox.Show("Empty Username!");
            }
            else
            {
                Properties.Settings.Default.Username = UsernameTexbox.Text;
                Properties.Settings.Default.Save();
                FortniteV1.Launch(PathTextBox.Text, UsernameTexbox.Text);
                GameLaunched form = new GameLaunched();
                form.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
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
                        PathTextBox.Text = dialog.FileName;
                        Properties.Settings.Default.Path = dialog.FileName;
                        Properties.Settings.Default.Save();
                    }

                }
            }
        }
    }
}
