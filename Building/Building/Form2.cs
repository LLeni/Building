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
        public Form2()
        {
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

        }


        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        Database database = new Database();

        private void button1_Click(object sender, EventArgs e)
        {
            database.OpenConnection();
            string query = "INSERT INTO Breaches(ID_FLOOR, LOCATION_BREACH, TOPIC_BREACH, DESCRIPTION_BREACH, DATE_BREACH, CONDITION_BREACH) VALUES (@ID_FLOOR, @LOCATION_BREACH, @TOPIC_BREACH,@DESCRIPTION_BREACH, @DATE_BREACH, @CONDITION_BREACH) ";
            SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            myCommand.Parameters.AddWithValue("@ID_FLOOR", iD_FLOORTextBox.Text);
            myCommand.Parameters.AddWithValue("@LOCATION_BREACH", lOCATION_BREACHTextBox.Text);
            myCommand.Parameters.AddWithValue("@TOPIC_BREACH", tOPIC_BREACHTextBox.Text);
            myCommand.Parameters.AddWithValue("@DESCRIPTION_BREACH", richTextBox1.Text);
            myCommand.Parameters.AddWithValue("@DATE_BREACH", dATE_BREACHDateTimePicker.Text);
            myCommand.Parameters.AddWithValue("@CONDITION_BREACH", 0);
            myCommand.ExecuteNonQuery();
            database.CloseConnection();
            this.Hide();
        }

        private void iD_FLOORLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
