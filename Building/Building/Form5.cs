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
    public partial class Form5 : Form
    {
        String nameForm;
        public Form5()
        {
            InitializeComponent();
        }

        public Form5(String nameForm)
        {
            InitializeComponent();
            this.nameForm = nameForm;
            switch (nameForm)
            {
                case "Этаж":
                    label1.Text = "Выберите этаж:";
                    label2.Text = "Удаление информации об этаже";
                    comboBox1.Visible = true;
                    comboBox2.Visible = false;
                    comboBox3.Visible = false;
                break;
                case "Офис":
                    label1.Text = "Выберите офис:";
                    label2.Text = "Удаление информации об офисе";
                    comboBox1.Visible = false;
                    comboBox2.Visible = true;
                    comboBox3.Visible = false;
                    break;
                case "Камера":
                    label1.Text = "Выберите камеру:";
                    label2.Text = "Удаление информации о камере";
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = true;
                    break;
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingDataSet.Cameras". При необходимости она может быть перемещена или удалена.
            this.camerasTableAdapter.Fill(this.buildingDataSet.Cameras);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingDataSet.Offices". При необходимости она может быть перемещена или удалена.
            this.officesTableAdapter.Fill(this.buildingDataSet.Offices);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingDataSet.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter.Fill(this.buildingDataSet.Floors);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingDataSet.Breaches". При необходимости она может быть перемещена или удалена.
            this.breachesTableAdapter.Fill(this.buildingDataSet.Breaches);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
