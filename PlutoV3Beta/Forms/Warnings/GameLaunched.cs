using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlutoV3Beta.Forms.Warnings
{
    public partial class GameLaunched : Form
    {
        private Timer closeTimer;
        public GameLaunched()
        {
            InitializeComponent();
            InitializeNotification();
        }
        private void InitializeNotification()
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.LightGray; // Set the background color
            this.Size = new Size(300, 100); // Set the size of the form
            this.TopMost = true; // Ensure the form is topmost
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10,
                                      Screen.PrimaryScreen.WorkingArea.Height - this.Height - 10); // Position it at the bottom right corner

            // Add a label to the form
            Label messageLabel = new Label
            {
                Text = "Fortnite launched",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point((this.Width - 150) / 2, (this.Height - 30) / 2)
            };
            this.Controls.Add(messageLabel);

            // Initialize and start the timer
            closeTimer = new Timer();
            closeTimer.Interval = 10000; // 10 seconds
            closeTimer.Tick += CloseTimer_Tick;
            closeTimer.Start();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer and close the form
            closeTimer.Stop();
            this.Close();
        }

        private void GameLaunched_Load(object sender, EventArgs e)
        {

        }
    }
}
