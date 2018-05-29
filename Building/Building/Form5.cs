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
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Cameras". При необходимости она может быть перемещена или удалена.
            this.camerasTableAdapter1.Fill(this.buldingDataSet2.Cameras);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            this.floorsTableAdapter1.Fill(this.buldingDataSet2.Floors);

            database = new Database();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите это удалить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                database.OpenConnection();
                string queryDelete;
                SQLiteCommand myCommandDelete = database.myConnection.CreateCommand();
                myCommandDelete.CommandType = CommandType.Text;
                switch (nameForm)
                {
                    case "Этаж":
                        queryDelete = "DELETE FROM Floors WHERE ID_FLOOR = @ID_FLOOR";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                        break;
                    case "Офис":
                        queryDelete = "DELETE FROM Offices WHERE ID_OFFICE = @ID_OFFICE";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_OFFICE", comboBox2.Text);
                        break;
                    case "Камера":
                        queryDelete = "DELETE FROM Cameras WHERE ID_CAMERA = @ID_CAMERA";
                        myCommandDelete.CommandText = queryDelete;
                        myCommandDelete.Parameters.AddWithValue("@ID_CAMERA", comboBox3.Text);
                        break;
                }
                myCommandDelete.ExecuteNonQuery();
                database.CloseConnection();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
