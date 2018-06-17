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
using System.Collections.ObjectModel;

namespace Building
{
    public partial class Form4 : Form
    {
        String data;
        String idCompany;
        ObservableCollection<String> collectionForRefresh;

        Database database;
        DataTable dataTableFloors;
        public Form4()
        {
            InitializeComponent();
        }

        public Form4(String data, ObservableCollection<String> collectionForRefresh, DataTable dataTableFloors)
        {
            InitializeComponent();
            this.data = data;
            this.collectionForRefresh = collectionForRefresh;
            this.dataTableFloors = dataTableFloors;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            database = new Database();

            comboBox1.DataSource = dataTableFloors;

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
                button1.Text = "Изменить";
                comboBox2.Visible = true;
                textBox3.Visible = false;

                loadInfo(Convert.ToString(dataTableFloors.Rows[0][0]));

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

        private void button1_Click(object sender, EventArgs e)
        {
            database.OpenConnection();

            if (data == "Добавление")
            {
                if (textBox5.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Проверка на наличие офиса
                    string queryCheckExist = "SELECT ID_OFFICE FROM Offices WHERE ID_OFFICE =  " + textBox3.Text;
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
                        MessageBox.Show("Данный офис существует, добавление невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        //Получение наибольшего значения идентификатора в таблице "Компании"
                        string queryIDCompany = "SELECT ID_COMPANY FROM Companies ORDER BY ID_COMPANY DESC LIMIT 1";
                        SQLiteCommand myCommandIDCompany = database.myConnection.CreateCommand();
                        myCommandIDCompany.CommandText = queryIDCompany;
                        myCommandIDCompany.CommandType = CommandType.Text;
                        SQLiteDataReader reader = myCommandIDCompany.ExecuteReader();
                        int IDCompany = 0;
                        while (reader.Read())
                        {
                            IDCompany = Convert.ToInt16(Convert.ToString(reader["ID_COMPANY"])) + 1;
                        }

                        // Таблица "Компании"
                        string query = "INSERT INTO Companies ('ID_COMPANY','NAME_COMPANY', 'DESCRIPTION', 'PHONE_COMPANY') VALUES (@ID_COMPANY, @NAME_COMPANY, @DESCRIPTION, @PHONE_COMPANY) ";
                        SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                        myCommand.Parameters.AddWithValue("@ID_COMPANY", IDCompany);
                        myCommand.Parameters.AddWithValue("@NAME_COMPANY", textBox5.Text);
                        myCommand.Parameters.AddWithValue("@DESCRIPTION", textBox4.Text);
                        myCommand.Parameters.AddWithValue("@PHONE_COMPANY", textBox6.Text);
                        myCommand.ExecuteNonQuery();

                        //Таблица "Офисы"
                        string query2 = "INSERT INTO Offices ('ID_OFFICE', 'ID_FLOOR', 'ID_COMPANY') VALUES (@ID_OFFICE, @ID_FLOOR, @ID_COMPANY) ";
                        SQLiteCommand myCommand2 = new SQLiteCommand(query2, database.myConnection);
                        myCommand2.Parameters.AddWithValue("@ID_OFFICE", textBox3.Text);
                        myCommand2.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        myCommand2.Parameters.AddWithValue("@ID_COMPANY", IDCompany);
                        myCommand2.ExecuteNonQuery();
                        MessageBox.Show("Сведения об офисе были успешно добавлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
            }
            else
            {
                if (textBox5.Text == "" || textBox4.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string queryUpdateCompany = "UPDATE Companies SET NAME_COMPANY = @NAME_COMPANY, DESCRIPTION = @DESCRIPTION, PHONE_COMPANY = @PHONE_COMPANY WHERE ID_COMPANY = @ID_COMPANY";
                SQLiteCommand myCommandUpdateCompany = database.myConnection.CreateCommand();
                myCommandUpdateCompany.CommandText = queryUpdateCompany;
                myCommandUpdateCompany.Parameters.AddWithValue("@ID_COMPANY", idCompany);
                myCommandUpdateCompany.Parameters.AddWithValue("@NAME_COMPANY", textBox5.Text);
                myCommandUpdateCompany.Parameters.AddWithValue("@DESCRIPTION", textBox4.Text);
                myCommandUpdateCompany.Parameters.AddWithValue("@PHONE_COMPANY", textBox6.Text);
                myCommandUpdateCompany.ExecuteNonQuery();

                MessageBox.Show("Сведения об офисе были изменены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            database.CloseConnection();
        }

        private void loadInfo(String IDFloor)
        {
            database.OpenConnection();

            string queryDataFloor = "SELECT ID_OFFICE FROM Offices WHERE ID_FLOOR = " + IDFloor;
            SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
            myCommandDataFloor.CommandText = queryDataFloor;
            myCommandDataFloor.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(Convert.ToString(reader["ID_OFFICE"]));
            }

            database.CloseConnection();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (data == "Редактирование")
            {
                comboBox2.Items.Clear();
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

                loadInfo(comboBox1.Text);
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            database.OpenConnection();

            // Вытаскиваем идентификатор компании
            string queryDataOffice = "SELECT ID_COMPANY FROM Offices WHERE ID_OFFICE =" + comboBox2.Text;
            SQLiteCommand myCommandDataOffice = database.myConnection.CreateCommand();
            myCommandDataOffice.CommandText = queryDataOffice;
            myCommandDataOffice.CommandType = CommandType.Text;
            SQLiteDataReader readerOffice = myCommandDataOffice.ExecuteReader();
            while (readerOffice.Read())
            {
                idCompany = Convert.ToString(readerOffice["ID_COMPANY"]);
            }

            // Извлекаем данные о компании
            string queryDataCompany = "SELECT * FROM Companies WHERE ID_COMPANY =" + idCompany;
            SQLiteCommand myCommandDataCompany = database.myConnection.CreateCommand();
            myCommandDataCompany.CommandText = queryDataCompany;
            myCommandDataCompany.CommandType = CommandType.Text;
            SQLiteDataReader readerCompany = myCommandDataCompany.ExecuteReader();
            while (readerCompany.Read())
            {
                textBox5.Text = Convert.ToString(readerCompany["NAME_COMPANY"]);
                textBox4.Text = Convert.ToString(readerCompany["DESCRIPTION"]);
                textBox6.Text = Convert.ToString(readerCompany["PHONE_COMPANY"]);

            }

            database.CloseConnection();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
