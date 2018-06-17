using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        DataTable dataTableFloors;
        String data;
        String pathWorkDirectory;
        ObservableCollection<String> collectionForRefresh;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(String data,  ObservableCollection<String> collectionForRefresh, DataTable dataTableFloors)
        {
            InitializeComponent();
            this.data = data;
            this.collectionForRefresh = collectionForRefresh;
            this.dataTableFloors = dataTableFloors;
        }


        public Form3(String data, String pathWorkDirectory, ObservableCollection<String> collectionForRefresh, DataTable dataTableFloors)
        {
            InitializeComponent();
            this.data = data;
            this.pathWorkDirectory = pathWorkDirectory;
            this.dataTableFloors = dataTableFloors;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Кнопка "Изменить"
        private void button1_Click(object sender, EventArgs e)
        {
            if (data == "Добавление")
            {
                if (textBox1.Text == "" || comboBox2.Text == "" || pictureBox1.Tag == null)
                {
                    MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    database.OpenConnection();
                    string queryCheckExist = "SELECT ID_FLOOR FROM Floors WHERE ID_FLOOR =  " + textBox1.Text;
                    SQLiteCommand myCommandCheckExist = database.myConnection.CreateCommand();
                    myCommandCheckExist.CommandText = queryCheckExist;
                    myCommandCheckExist.CommandType = CommandType.Text;
                    SQLiteDataReader reader = myCommandCheckExist.ExecuteReader();
                    Boolean isExist = false;
                    while (reader.Read())
                    {
                        isExist = true;   
                    }

                    if (isExist)
                    {
                        MessageBox.Show("Данный этаж существует, добавление невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        Boolean isRoof = true;
                        //Проверка на наличие крыши
                        if (comboBox2.Text == "Крыша")
                        {
                            string queryRoofExist = "SELECT CATEGORY_FLOOR FROM Floors WHERE CATEGORY_FLOOR =  '" + comboBox2.Text + "'";
                            SQLiteCommand myCommandRoofExist = database.myConnection.CreateCommand();
                            myCommandRoofExist.CommandText = queryRoofExist;
                            myCommandRoofExist.CommandType = CommandType.Text;
                            SQLiteDataReader readerRoof = myCommandRoofExist.ExecuteReader();
                            isExist = false;
                            while (readerRoof.Read())
                            {
                                isExist = true;
                            }
                         } else
                        {
                            isRoof = false;
                        }
                        if (isExist || isRoof)
                        {
                            MessageBox.Show("Информация о крыше уже есть. Невозможно добавить вторую", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            string query = "INSERT INTO Floors ('ID_FLOOR', 'CATEGORY_FLOOR', 'PATH') VALUES (@ID_FLOOR, @CATEGORY_FLOOR, @PATH) ";
                            SQLiteCommand myCommandInsertFloor = database.myConnection.CreateCommand();
                            myCommandInsertFloor.CommandText = query;
                            myCommandInsertFloor.Parameters.AddWithValue("@ID_FLOOR", textBox1.Text);
                            myCommandInsertFloor.Parameters.AddWithValue("@CATEGORY_FLOOR", comboBox2.Text);
                            myCommandInsertFloor.Parameters.AddWithValue("@PATH", pictureBox1.Tag);
                            myCommandInsertFloor.ExecuteNonQuery();

                            database.CloseConnection();

                            MessageBox.Show("Информация была добавлена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DataRow row = dataTableFloors.NewRow();
                            row[0] = textBox1.Text;
                            row[1] = comboBox2.Text;
                            row[2] = pictureBox1.Tag;
                            dataTableFloors.Rows.Add(row);
                            dataTableFloors.DefaultView.Sort = "ID_FLOOR ASC";

                            collectionForRefresh[0] = "А";
                        }
                    }
                }
            }
            else
            {
                if (comboBox2.Text == "" || pictureBox1.Tag == null)
                {
                    MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
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

                    MessageBox.Show("Информация была изменена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Кнопка "Выход"
        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Tag = null;
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
            database = new Database();
            label4.Text = data + " информации об этаже";
            if (data == "Добавление")
            {
                button1.Text = "Добавить";
                textBox1.Visible = true;
                comboBox1.Visible = false;
            }
            else
            {
                button1.Text = "Изменить";
                textBox1.Visible = false;
                comboBox1.Visible = true;

                comboBox1.DataSource = dataTableFloors;
                loadInfo(Convert.ToString(dataTableFloors.Rows[0][0]));

            }
 
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

        private void loadInfo(String IDFloor)
        {
            database.OpenConnection();

            string queryDataFloor = "SELECT * FROM Floors WHERE ID_FLOOR = " + IDFloor;
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
                label14.Visible = false;
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

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadInfo(comboBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 16)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 45 && textBox1.Text == "")
            {
                e.Handled = false;
            }
        }
    }
}
