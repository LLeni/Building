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
                    label1.Location = new Point(label1.Location.X - 15, label1.Location.Y);
                    label2.Text = "Удаление информации о камере";
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = true;
                    break;
            }
        }

        Database database;
        private void Form5_Load(object sender, EventArgs e)
        {
            database = new Database();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter1.Fill(this.buldingDataSet2.Floors);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.officesTableAdapter1.Fill(this.buldingDataSet2.Offices);

            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.camerasTableAdapter1.Fill(this.buldingDataSet2.Cameras);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String partMessage = null;
            switch (nameForm)
            {
                case "Этаж":
                    partMessage = " этом этаже";
                    break;
                case "Офис":
                    partMessage = " этом офисе";
                    break;
                case "Камера":
                    partMessage = " этой камере";
                    break;
            }
            DialogResult dialogResult = MessageBox.Show("Вся информация об" + partMessage + "будет удалена! Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                database.OpenConnection();
                string queryDelete;
                string queryIDCompany;
                string queryIDOffice;
                SQLiteCommand myCommandDelete = database.myConnection.CreateCommand();
                SQLiteCommand myCommandIDCompany;
                SQLiteCommand myCommandIDOffice;
                myCommandDelete.CommandType = CommandType.Text;
                SQLiteDataReader reader;
                switch (nameForm)
                {
                    case "Этаж":
                        //TODO: Должна удалятся вся информация об этаже

                        //Удаление этажа
                        queryDelete = "DELETE FROM Floors WHERE ID_FLOOR = @ID_FLOOR";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        myCommandDelete.ExecuteNonQuery();

                        //Выборка офисов на этаже
                        queryIDOffice = "SELECT ID_OFFICE FROM Offices WHERE ID_FLOOR = @ID_FLOOR";
                        myCommandIDOffice = database.myConnection.CreateCommand();
                        myCommandIDOffice.CommandText = queryIDOffice;
                        myCommandIDOffice.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        reader = myCommandIDOffice.ExecuteReader();
                        while (reader.Read())
                        {
                            //Поиск компании связанной с текущим найденным офисом
                            queryIDCompany = "SELECT ID_COMPANY FROM Offices WHERE ID_OFFICE = @ID_OFFICE";
                            myCommandIDCompany = database.myConnection.CreateCommand();
                            myCommandIDCompany.CommandText = queryIDCompany;
                            myCommandIDCompany.Parameters.AddWithValue("@ID_OFFICE", Convert.ToString(reader["ID_OFFICE"]));
                            reader = myCommandIDCompany.ExecuteReader();
                            while (reader.Read())
                            {
                                //Удаление текущей выбранной компании
                                queryDelete = "DELETE FROM Companies WHERE ID_COMPANY = @ID_COMPANY";
                                myCommandDelete.CommandText = queryDelete;
                                myCommandDelete.Parameters.AddWithValue("@ID_COMPANY", Convert.ToString(reader["ID_COMPANY"]));
                                myCommandDelete.ExecuteNonQuery();
                            }
                        }

                        //Удаление офисов на этаже
                        queryDelete = "DELETE FROM Offices WHERE ID_FLOOR = @ID_FLOOR";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        myCommandDelete.ExecuteNonQuery();

                        //Удаление камер на этаже
                        queryDelete = "DELETE FROM Cameras WHERE ID_FLOOR = @ID_FLOOR";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        myCommandDelete.ExecuteNonQuery();

                        break;
                    case "Офис":
                        //TODO: Должна удаляться вся информация об офисе (в том числе и компания)

                        queryIDCompany = "SELECT ID_COMPANY FROM Offices WHERE ID_OFFICE = @ID_OFFICE";
                        myCommandIDCompany = database.myConnection.CreateCommand();
                        myCommandIDCompany.CommandText = queryIDCompany;
                        myCommandIDCompany.Parameters.AddWithValue("@ID_OFFICE", comboBox2.Text);
                        reader = myCommandIDCompany.ExecuteReader();
                        while (reader.Read())
                        {
                            //Удаление компании связанной с текущим выбранным офисом
                            queryDelete = "DELETE FROM Companies WHERE ID_COMPANY = @ID_COMPANY";
                            myCommandDelete.CommandText = queryDelete;
                            myCommandDelete.Parameters.AddWithValue("@ID_COMPANY", Convert.ToString(reader["ID_COMPANY"]));
                            myCommandDelete.ExecuteNonQuery();
                        }

                        //Удаление офиса
                        queryDelete = "DELETE FROM Offices WHERE ID_OFFICE = @ID_OFFICE";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_OFFICE", comboBox2.Text);
                        myCommandDelete.ExecuteNonQuery();

                        break;
                    case "Камера":
                        queryDelete = "DELETE FROM Cameras WHERE ID_CAMERA = @ID_CAMERA";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_CAMERA", comboBox3.Text);
                        myCommandDelete.ExecuteNonQuery();
                        break;
                }

                database.CloseConnection();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
