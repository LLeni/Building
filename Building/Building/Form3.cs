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
        Database database;
        String data;
        String pathWorkDirectory;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(String data)
        {
            InitializeComponent();
            this.data = data;
        }


        public Form3(String data, String pathWorkDirectory)
        {
            InitializeComponent();
            this.data = data;
            this.pathWorkDirectory = pathWorkDirectory;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Кнопка "Изменить"
        private void button1_Click(object sender, EventArgs e)
        {
            database.OpenConnection();

            string queryUpdateFloor = "UPDATE Floors SET CATEGORY_FLOOR = @CATEGORY_FLOOR, PATH = @PATH WHERE ID_FLOOR = @ID_FLOOR";
            SQLiteCommand myCommandUpdateFloor = database.myConnection.CreateCommand();
            myCommandUpdateFloor.CommandText = queryUpdateFloor;
            myCommandUpdateFloor.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
            myCommandUpdateFloor.Parameters.AddWithValue("@CATEGORY_FLOOR", comboBox2.Text);
            myCommandUpdateFloor.Parameters.AddWithValue("@PATH", pictureBox1.Tag);
            myCommandUpdateFloor.ExecuteNonQuery();

            database.CloseConnection();
        }

        //Кнопка "Выход"
        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            label14.Visible = true;
            button7.Visible = false;
        }

        Image image;
        private void button3_Click(object sender, EventArgs e)
        {
           }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter.Fill(this.buldingDataSet2.Floors);
            label4.Text = data + " информации об этаже";
            if (data == "Добавление")
            {
                button1.Text = "Добавить";
                textBox1.Visible = true;
                comboBox1.Visible = false;
               // label4.Location = new Point(label1.Location.X + 20, label1.Location.Y);
            }
            else
            {
                button1.Text = "Редактировать";
                textBox1.Visible = false;
                comboBox1.Visible = true;
            }
            database = new Database();
            comboBox1.DisplayMember = "ID_FLOOR";
            comboBox1.ValueMember = "ID_FLOOR";
        }

        private void label14_Click(object sender, EventArgs e)
        {
            openDialog();
        }

        private void openDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    label14.Visible = false;
                    button7.Visible = true;

                    //Сохранение пути изображения в свойстве компонента PictureBox
                    pictureBox1.Tag = ofd.FileName;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label14.Visible = true;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openDialog();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pictureBox1.Tag = "";

            database.OpenConnection();

            //Получение наибольшего значения идентификатора в таблице "Компании"
            string queryDataFloor = "SELECT * FROM Floors WHERE ID_FLOOR = " + comboBox1.Text;
            SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
            myCommandDataFloor.CommandText = queryDataFloor;
            myCommandDataFloor.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Text = Convert.ToString(reader["CATEGORY_FLOOR"]);
                pictureBox1.Tag = Convert.ToString(reader["PATH"]);
            }
            try
            {
                label14.Visible = false; ;
                button7.Visible = true;
                image = Image.FromFile(Convert.ToString(pictureBox1.Tag));

            }
            catch
            {
                try
                {
                    image = Image.FromFile(pathWorkDirectory + Convert.ToString(pictureBox1.Tag));
                }
                catch
                {
                    label14.Visible = true;
                    button7.Visible = false;
                    MessageBox.Show("Проблема с путем плана этажа!");

                }

            }

            pictureBox1.Image = (Image)image;

            database.CloseConnection();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
