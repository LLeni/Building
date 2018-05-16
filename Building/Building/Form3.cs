using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        Database database;
        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter.Fill(this.dataSet1.Floors);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Breaches". При необходимости она может быть перемещена или удалена.
            this.breachesTableAdapter.Fill(this.dataSet1.Breaches);

            database = new Database();
            comboBox1.DisplayMember = "ID_FLOOR";
            comboBox1.ValueMember = "ID_FLOOR";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        Image image;
        private void button3_Click(object sender, EventArgs e)
        {
            database.OpenConnection();

            //Получение наибольшего значения идентификатора в таблице "Компании"
            string queryDataFloor = "SELECT * FROM Floors WHERE ID_FLOOR = " + comboBox1.Text;
            SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
            myCommandDataFloor.CommandText = queryDataFloor;
            myCommandDataFloor.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = Convert.ToString(reader["ID_FLOOR"]);
                comboBox2.Text = Convert.ToString(reader["CATEGORY_FLOOR"]);
                pictureBox1.Tag = Convert.ToString(reader["PATH"]);
            }
            try
            {
                image = Image.FromFile(Convert.ToString(pictureBox1.Tag));
            } catch
            {
                MessageBox.Show("Проблема с путем плана этажа!");
            }
            pictureBox1.Image = (Image) image;

        }
    }
}
