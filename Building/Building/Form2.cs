using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace Building
{
    public partial class Form2 : Form
    {
        int a = 5;
        DataTable dataTableFloors;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(DataTable dataTableBreaches, DataTable dataTableFloors)
        {
            this.dataTableBreaches = dataTableBreaches;
            this.dataTableFloors = dataTableFloors;
            InitializeComponent();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(600, 400);
        }

        private void breachesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.breachesBindingSource.EndEdit();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buldingDataSet2.Floors". При необходимости она может быть перемещена или удалена.
            // this.floorsTableAdapter.Fill(this.buldingDataSet2.Floors);

            comboBox1.DataSource = dataTableFloors;
        }


        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        Database database = new Database();
        DataTable dataTableBreaches = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (lOCATION_BREACHTextBox.Text == "" || tOPIC_BREACHTextBox.Text == "" || tOPIC_BREACHTextBox.Text == "" || richTextBox1.Text == "" || dATE_BREACHDateTimePicker.Text == "")
            {
                MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                database.OpenConnection();


                string queryIDBreaches = "SELECT ID_BREACH FROM Breaches ORDER BY ID_BREACH DESC LIMIT 1";
                SQLiteCommand myCommandIDBreaches = database.myConnection.CreateCommand();
                myCommandIDBreaches.CommandText = queryIDBreaches;
                myCommandIDBreaches.CommandType = CommandType.Text;
                SQLiteDataReader reader = myCommandIDBreaches.ExecuteReader();
                int IDBreaches = 0;
                while (reader.Read())
                {
                    IDBreaches = Convert.ToInt16(Convert.ToString(reader["ID_BREACH"])) + 1;
                }

                //Форматируем дату
                string[] words = dATE_BREACHDateTimePicker.Text.Split(new char[] { ' ' });
                string numberMonthStr = "";
                switch (words[1])
                {
                    case "января":
                        numberMonthStr = "01";
                        break;
                    case "февраля":
                        numberMonthStr = "02";
                        break;
                    case "марта":
                        numberMonthStr = "03";
                        break;
                    case "апреля":
                        numberMonthStr = "04";
                        break;
                    case "мая":
                        numberMonthStr = "05";
                        break;
                    case "июня":
                        numberMonthStr = "06";
                        break;
                    case "июля":
                        numberMonthStr = "07";
                        break;
                    case "августа":
                        numberMonthStr = "08";
                        break;
                    case "сентября":
                        numberMonthStr = "09";
                        break;
                    case "октября":
                        numberMonthStr = "10";
                        break;
                    case "ноября":
                        numberMonthStr = "11";
                        break;
                    case "декабря":
                        numberMonthStr = "12";
                        break;
                }

                if(words[0].Length == 1)
                {
                    words[0] = '0' + words[0];
                }
                string formattedDate = words[0] + '.' + numberMonthStr + '.' + words[2];

                string query = "INSERT INTO Breaches(ID_BREACH, ID_FLOOR, LOCATION_BREACH, TOPIC_BREACH, DESCRIPTION_BREACH, DATE_BREACH, CONDITION_BREACH) VALUES (@ID_BREACH, @ID_FLOOR, @LOCATION_BREACH, @TOPIC_BREACH,@DESCRIPTION_BREACH, @DATE_BREACH, @CONDITION_BREACH) ";
                SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                myCommand.Parameters.AddWithValue("@ID_BREACH", IDBreaches);
                myCommand.Parameters.AddWithValue("@ID_FLOOR", comboBox1.Text);
                myCommand.Parameters.AddWithValue("@LOCATION_BREACH", lOCATION_BREACHTextBox.Text);
                myCommand.Parameters.AddWithValue("@TOPIC_BREACH", tOPIC_BREACHTextBox.Text);
                myCommand.Parameters.AddWithValue("@DESCRIPTION_BREACH", richTextBox1.Text);
                myCommand.Parameters.AddWithValue("@DATE_BREACH", formattedDate);
                myCommand.Parameters.AddWithValue("@CONDITION_BREACH", 0);
                myCommand.ExecuteNonQuery();


                database.CloseConnection();

                DataRow row = dataTableBreaches.NewRow();
                row[0] = IDBreaches; //TODO: !!!
                row[1] = comboBox1.Text;
                row[2] = lOCATION_BREACHTextBox.Text;
                row[3] = tOPIC_BREACHTextBox.Text;
                row[4] = richTextBox1.Text;
                row[5] = formattedDate;
                row[6] = 0;
                dataTableBreaches.Rows.Add(row);

                MessageBox.Show("Информация о нарушении была добавлена! ", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
            }
        }

        private void iD_FLOORLabel_Click(object sender, EventArgs e)
        {

        }

        private void lOCATION_BREACHTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
