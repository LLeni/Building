using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

            this.Width = SystemInformation.PrimaryMonitorSize.Width; 
            this.Height = SystemInformation.PrimaryMonitorSize.Height;
        }

        private void Form7_Leave(object sender, EventArgs e)
        {

        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
