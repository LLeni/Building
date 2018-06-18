using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            string kniga1 = Directory.GetCurrentDirectory() + @"\Resources\w.pdf";
          //  axAcroPDF1.LoadFile(kniga1);
          //  axAcroPDF1.src = kniga1;
         //   axAcroPDF1.setShowToolbar(false);
         //   axAcroPDF1.setView("FitH");
          ///  axAcroPDF1.setLayoutMode("auto");
          //  axAcroPDF1.Show();
        }
    }
}
