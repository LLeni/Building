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
    public partial class Form6 : Form
    {
        String data;
        public Form6()
        {
            InitializeComponent();
        }

        public Form6(String data)
        {
            InitializeComponent();
            this.data = data;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter.Fill(this.buldingDataSet2.Floors);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Cameras". При необходимости она может быть перемещена или удалена.
            this.camerasTableAdapter.Fill(this.buldingDataSet2.Cameras);
            label1.Text = data + " информации о камере";
            if (data == "Добавление")
            {
                button1.Text = "Добавить";
                label1.Location = new Point(label1.Location.X + 20, label1.Location.Y);
            }
            else
            {
                button1.Text = "Редактировать";
                label1.Text = "Редактирование информации о камере";
            }
        }
    }
}
