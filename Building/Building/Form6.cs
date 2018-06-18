using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace Building
{
    public partial class Form6 : Form
    {
        String data;
        ObservableCollection<String> collectionForRefresh;
        DataTable dataTableFloors;
        DataTable dataTableCameras;
        Database database;
        public Form6()
        {
            InitializeComponent();
        }

        public Form6(String data, ObservableCollection<String> collectionForRefresh, DataTable dataTableFloors, DataTable dataTableCameras)
        {
            InitializeComponent();
            this.data = data;
            this.collectionForRefresh = collectionForRefresh;
            this.dataTableFloors = dataTableFloors;
            this.dataTableCameras = dataTableCameras;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.OpenConnection();
            
            if (data == "Добавление")
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                {
                    MessageBox.Show("Вы не все ввели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    database.OpenConnection();

                    //Проверка на наличие камеры
                    string queryCheckExist = "SELECT IP_CAMERA FROM Cameras WHERE IP_CAMERA =  '" + textBox1.Text + "'";
                    SQLiteCommand myCommandCheckExist = database.myConnection.CreateCommand();
                    myCommandCheckExist.CommandText = queryCheckExist;
                    myCommandCheckExist.CommandType = CommandType.Text;
                    SQLiteDataReader readerCheckExist = myCommandCheckExist.ExecuteReader();
                    Boolean isExist = false;
                    while (readerCheckExist.Read())
                    {
                        isExist = true;
                    }

                    if (isExist)
                    {
                        MessageBox.Show("Данная камера с таким IP-адресом уже существует, добавление невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Получение наибольшего значения идентификатора в таблице "Камеры"
                        string queryIDCompany = "SELECT ID_CAMERA FROM Cameras ORDER BY ID_CAMERA DESC LIMIT 1";
                        SQLiteCommand myCommandIDCompany = database.myConnection.CreateCommand();
                        myCommandIDCompany.CommandText = queryIDCompany;
                        myCommandIDCompany.CommandType = CommandType.Text;
                        SQLiteDataReader reader = myCommandIDCompany.ExecuteReader();
                        int IDCamera = 0;
                        while (reader.Read())
                        {
                            IDCamera = Convert.ToInt16(Convert.ToString(reader["ID_CAMERA"])) + 1;
                        }

                        string query = "INSERT INTO Cameras ('ID_CAMERA', 'ID_FLOOR', 'IP_CAMERA', 'MAC_CAMERA', 'DESCRIPTION') VALUES (@ID_CAMERA, @ID_FLOOR, @IP_CAMERA, @MAC_CAMERA, @DESCRIPTION) ";
                        SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                        myCommand.Parameters.AddWithValue("@ID_CAMERA", IDCamera);
                        myCommand.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        myCommand.Parameters.AddWithValue("@IP_CAMERA", textBox1.Text);
                        myCommand.Parameters.AddWithValue("@MAC_CAMERA", textBox2.Text);
                        myCommand.Parameters.AddWithValue("@DESCRIPTION", textBox3.Text);
                        myCommand.ExecuteNonQuery();

                        MessageBox.Show("Информация была добавлена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DataRow row = dataTableCameras.NewRow();
                        row[0] = IDCamera;
                        row[1] = comboBox1.Text;
                        row[2] = textBox1.Text;
                        row[3] = textBox2.Text;
                        row[4] = textBox3.Text;
                        dataTableCameras.Rows.Add(row);
                        dataTableCameras.DefaultView.Sort = "ID_CAMERA ASC";

                        collectionForRefresh[0] = "А";
                    }
                }
            }
            else
            {
                string queryUpdateCamera = "UPDATE Cameras SET MAC_CAMERA = @MAC_CAMERA, DESCRIPTION = @DESCRIPTION WHERE IP_CAMERA= @IP_CAMERA";
                SQLiteCommand myCommandUpdateCamera = database.myConnection.CreateCommand();
                myCommandUpdateCamera.CommandText = queryUpdateCamera;
                myCommandUpdateCamera.Parameters.AddWithValue("@IP_CAMERA", comboBox2.Text);
                myCommandUpdateCamera.Parameters.AddWithValue("@MAC_CAMERA", textBox2.Text);
                myCommandUpdateCamera.Parameters.AddWithValue("@DESCRIPTION", textBox3.Text);
                myCommandUpdateCamera.ExecuteNonQuery();

                MessageBox.Show("Сведения о камере были изменены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                collectionForRefresh[0] = "А";
            }
            database.CloseConnection();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            database = new Database();
            comboBox1.DataSource = dataTableFloors;
            label1.Text = data + " информации о камере";
            if (data == "Добавление")
            {
                button1.Text = "Добавить";
                label1.Location = new Point(label1.Location.X + 20, label1.Location.Y);
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }
            else
            {
                button1.Text = "Изменить";
                label1.Text = "Редактирование информации о камере";
                textBox1.Visible = false;
                comboBox2.Visible = true;

                loadInfo(Convert.ToString(dataTableFloors.Rows[0][0]));
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {

            database.OpenConnection();

            string queryDataCamera = "SELECT * FROM Cameras WHERE IP_CAMERA = '" + comboBox2.Text + "'";
            SQLiteCommand myCommandDataCamera = database.myConnection.CreateCommand();
            myCommandDataCamera.CommandText = queryDataCamera;
            myCommandDataCamera.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataCamera.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = Convert.ToString(reader["MAC_CAMERA"]);
                textBox3.Text = Convert.ToString(reader["DESCRIPTION"]);
            }

            database.CloseConnection();
        }

        private void loadInfo(String IDFloor)
        {
            database.OpenConnection();

            string queryDataCamera = "SELECT IP_CAMERA FROM Cameras WHERE ID_FLOOR = " + IDFloor;
            SQLiteCommand myCommandDataCamera = database.myConnection.CreateCommand();
            myCommandDataCamera.CommandText = queryDataCamera;
            myCommandDataCamera.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataCamera.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(Convert.ToString(reader["IP_CAMERA"]));
            }

            database.CloseConnection();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (data.Equals("Редактирование"))
            {
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox2.Items.Clear();


                loadInfo(comboBox1.Text);
    
            }
        }
    }
}
