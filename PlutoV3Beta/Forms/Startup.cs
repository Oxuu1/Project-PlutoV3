using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core_Launcher_Rewrite_again.Resources;
using PlutoV3Beta.Forms.Warnings;

namespace PlutoV3Beta.Forms
{
    public partial class Startup : Form
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
        public Startup()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Startup_Load(object sender, EventArgs e)
        {
            Lawin.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            PlutoForm form = new PlutoForm();
            form.Show();
            this.Hide();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            GameLaunched form = new GameLaunched();
            form.Show();
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            PlutoForm form = new PlutoForm();
            form.Show();
            this.Hide();
        }

        private void siticoneButton6_Click(object sender, EventArgs e)
        {
            PlutoHost form = new PlutoHost();
            form.Show();
            this.Hide();
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            PlutoSinglePlayer form = new PlutoSinglePlayer();
            form.Show();
            this.Hide();
        }

        private void siticoneButton7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
