using Patagames.Pdf.Net;
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
            PdfCommon.Initialize();
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // pdfViewer1.LoadDocument("C:/Users/Ironik/Desktop/hh/w.pdf");
            pdfViewer1.LoadDocument(@"Resources/справка.pdf");
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
