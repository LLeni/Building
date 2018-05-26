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
    public partial class Form4 : Form
    {
        String data;
        public Form4()
        {
            InitializeComponent();
        }

        public Form4(String data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingDataSet.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter.Fill(this.buildingDataSet.Floors);
            label1.Text = data + " информации об офисе";
            if (data == "Добавление")
            {
                button1.Text = "Добавить";
                comboBox2.Visible = false;
                textBox3.Visible = true;
                label1.Location = new Point(label1.Location.X + 20, label1.Location.Y);
            }
            else
            {
                button1.Text = "Редактировать";
                comboBox2.Visible = true;
                textBox3.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
