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
using System.IO;
using System.Data.Common;
using System.Data.SQLite;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Building
{
    public partial class Form1 : Form
    {
        public FilterInfoCollection videoDevices;
        public VideoCaptureDevice videoSource;

        public Form1()
        {
            InitializeComponent();

        }
        public Boolean start = false;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)           //Отображение панели нарушений
        {
            main_panel.Visible = false;                             //Скрывается главная вкладка
            add.Visible = false;                                 //Скрывается вкладка добавления этажей
            camera.Visible = false;                                 //Скрывается вкладка камер
            error.Visible = true;                                  //Отображается вкладка нарушений
            скрытьОкноИнформацииToolStripMenuItem.Enabled = false;  //Блокируется возможность добавления окна информации
            скрытьОкноИнформацииToolStripMenuItem.Checked = false;  //Исчезнавение галочки окна информации
            splitContainer2.Panel2Collapsed = true;                 //Скрывается окно информации
            splitContainer2.Panel2.Hide();
            панельКамерToolStripMenuItem.Enabled = false;           //Блокируется возможность добавления панели камер
            панельКамерToolStripMenuItem.Checked = false;           //Исчезнавение галочки панели камер
            splitContainer1.Panel2Collapsed = true;                 //Скрывается панель камер
            splitContainer1.Panel2.Hide();
            splitContainer3.Panel1Collapsed = true;                 //Скрывается поисковая панель
            splitContainer3.Panel1.Hide();
        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
    
        }

        private void скрытьОкноИнформацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (скрытьОкноИнформацииToolStripMenuItem.Checked)
            {
                splitContainer2.Panel2Collapsed = false;
                splitContainer2.Panel2.Show();
            }
            else
            {
                splitContainer2.Panel2Collapsed = true;
                splitContainer2.Panel2.Hide();
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        }

        Database database;
        private void Form1_Shown(object sender, EventArgs e)
        {
            Form1 firstForm = new Form1();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;


            database = new Database();

            //Таблица "Нарушения"
            DataSet ds = new DataSet();
            database.OpenConnection();
            string query = "Select * From Breaches";
            //SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            try
            {

                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, database.myConnection))
                    {
                        da.Fill(ds);
                        breachesDataGridView.DataSource = ds.Tables[0].DefaultView;
                    }
                
            }
            catch (Exception err)
            {
            }

            //Настройка столбцов таблицы "Нарушения
            breachesDataGridView.Columns[0].HeaderText = "Номер нарушения";
            breachesDataGridView.Columns[1].HeaderText = "Номер этажа";
            breachesDataGridView.Columns[2].HeaderText = "Местоположение";
            breachesDataGridView.Columns[3].HeaderText = "Тема";
            breachesDataGridView.Columns[4].HeaderText = "Описание";
            breachesDataGridView.Columns[5].HeaderText = "Дата";
            breachesDataGridView.Columns[6].HeaderText = "Исправлено";

            breachesDataGridView.Columns[0].ReadOnly = true;
            breachesDataGridView.Columns[1].ReadOnly = true;
            breachesDataGridView.Columns[2].ReadOnly = true;
            breachesDataGridView.Columns[3].ReadOnly = true;
            breachesDataGridView.Columns[4].ReadOnly = true;
            breachesDataGridView.Columns[5].ReadOnly = true;
        }


        private void панельКамерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (панельКамерToolStripMenuItem.Checked)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
            }
        }

        private void добавитьЭтажToolStripMenuItem_Click(object sender, EventArgs e)    //Отображение панели добавления этажей
        {
            add.Visible = true;                                  //Отображение панели добавления этажей
            main_panel.Visible = false;                             //Скрытие главной вкладка
            error.Visible = false;                                 //Скрытие вкладка нарушений
            camera.Visible = false;                                 //Скрывается вкладка камер
            скрытьОкноИнформацииToolStripMenuItem.Enabled = false;  //Блокируется возможность добавления окна информации
            скрытьОкноИнформацииToolStripMenuItem.Checked = false;  //Исчезнавение галочки окна информации
            splitContainer2.Panel2Collapsed = true;                 //Скрывается окно информации
            splitContainer2.Panel2.Hide();
            панельКамерToolStripMenuItem.Enabled = false;           //Блокируется возможность добавления панели камер
            панельКамерToolStripMenuItem.Checked = false;           //Исчезнавение галочки панели камер
            splitContainer1.Panel2Collapsed = true;                 //Скрывается панель камер
            splitContainer1.Panel2.Hide();
            splitContainer3.Panel1Collapsed = true;                 //Скрывается поисковая панель
            splitContainer3.Panel1.Hide(); 
        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)     //Отображение главной панели
        {
            main_panel.Visible = true;                              //Отображается главная вкладка
            add.Visible = false;                                 //Скрывается вкладка добавления этажей
            error.Visible = false;                                 //Скрывается вкладка нарушений
            camera.Visible = false;                                 //Скрывается вкладка камер
            splitContainer3.Panel1Collapsed = false;                //Отображается поисковой панели
            splitContainer3.Panel1.Show();
            скрытьОкноИнформацииToolStripMenuItem.Enabled = true;   //Снятие блокировки с возможности добавления окна информации
            панельКамерToolStripMenuItem.Enabled = true;            //Снятие блокировки с возможности добавления панели камер
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingNewDataSet.Breaches". При необходимости она может быть перемещена или удалена.
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    listBox1.Items.Add(device.Name);
                }
                listBox1.SelectedIndex = 0;
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

   
        private void button2_Click_1(object sender, EventArgs e)
        {
            //Получаем ссылку на кнопку, на которую нажали
            Button oldbutton = (Button)sender;
            //Создаем новую кнопку
            Button newbutton = new Button();
            //Меняем текст на новой кнопке
            newbutton.Text = "Кнопка №2";
            newbutton.Width = oldbutton.Width; 
            newbutton.Height = oldbutton.Height;
            //Размещаем ее ниже (на 10px) кнопки, на которую мы нажали
            newbutton.Location = new Point(oldbutton.Location.X, oldbutton.Location.Y + oldbutton.Height + 1);
            //Добавляем событие нажатия на новую кнопку 
            //(то же что и при нажатии на исходную)
            newbutton.Click += new EventHandler(button1_Click);
            //Добавляем элемент на форму
            main_panel.Controls.Add(newbutton);


        }

        private void breachesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.breachesBindingSource.EndEdit();
        }

        private void breachesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.ShowInTaskbar = false;                           //скрыть вторую форму из панели задач   
            secondForm.ShowDialog();
        }

        private void splitContainer5_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer5_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void камерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;                             //Скрывается главная вкладка
            add.Visible = false;                                 //Скрывается вкладка добавления этажей
            error.Visible = false;                                 //Скрывается вкладка нарушений
            camera.Visible = true;                                  //Отображается вкладка камер   
            скрытьОкноИнформацииToolStripMenuItem.Enabled = false;  //Блокируется возможность добавления окна информации
            скрытьОкноИнформацииToolStripMenuItem.Checked = false;  //Исчезнавение галочки окна информации
            splitContainer2.Panel2Collapsed = true;                 //Скрывается окно информации
            splitContainer2.Panel2.Hide();
            панельКамерToolStripMenuItem.Enabled = false;           //Блокируется возможность добавления панели камер
            панельКамерToolStripMenuItem.Checked = false;           //Исчезнавение галочки панели камер
            splitContainer1.Panel2Collapsed = true;                 //Скрывается панель камер
            splitContainer1.Panel2.Hide();
            splitContainer3.Panel1Collapsed = true;                 //Скрывается поисковая панель
            splitContainer3.Panel1.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            label14.Visible = true;
            button7.Visible = false;
        }

        private void редактироватьЭтажToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void камерыToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        string currentFloor;
        private void button3_Click_1(object sender, EventArgs e)
        {
 
            if (textBox2.Text.Equals("") || pictureBox1.Tag == null)
            {
                MessageBox.Show("Вы не все ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label21.Text = "Офис находится на этаже";       
                label19.Text = textBox2.Text;                   //добавляем номер этажа в графу "офис находится на этаже".

                // Таблица "Этажи"
                string query = "INSERT INTO Floors ('ID_FLOOR', 'CATEGORY_FLOOR', 'PATH') VALUES (@ID_FLOOR, @CATEGORY_FLOOR, @PATH) ";
                SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                database.OpenConnection();
                myCommand.Parameters.AddWithValue("@ID_FLOOR", textBox2.Text);
                myCommand.Parameters.AddWithValue("@CATEGORY_FLOOR", comboBox2.Text);
                myCommand.Parameters.AddWithValue("@PATH", pictureBox1.Tag);
                myCommand.ExecuteNonQuery();
                database.CloseConnection();
                currentFloor = textBox2.Text;
                MessageBox.Show("Сведения об этаже были успешно добавлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
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

        private void этажToolStripMenuItem_Click(object sender, EventArgs e)        //Вызов формы "Редактировать этаж"   
        {
            Form3 thirdForm = new Form3();
            thirdForm.ShowInTaskbar = false;                                        //скрыть вторую форму из панели задач
            thirdForm.Show();
            thirdForm.Text = "Редактировать информацию об этаже";
            // thirdForm.button1.Text = "2";   //ошибку с уровнем защиты. Разобраться и переименовать кнопку на 3-ей форме на "Выбрать"
            //нужно создать глобальную переменную и в этой строчке присвоить ей 1.
        }

        private void офисыToolStripMenuItem_Click(object sender, EventArgs e)       //Вызов формы "Редактировать офис"
        {
            Form3 thirdForm = new Form3();
            thirdForm.ShowInTaskbar = false;                                        //скрыть вторую форму из панели задач
            thirdForm.Show();
            thirdForm.Text = "Редактировать информацию об офисе";
            // thirdForm.button1.Text = "2";   //ошибку с уровнем защиты. Разобраться и переименовать кнопку на 3-ей форме на "Выбрать"
            //нужно создать глобальную переменную и в этой строчке присвоить ей 1.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentFloor == null)
            {
                MessageBox.Show("Вначале необходимо добавить сведения об этаже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox3.Text.Equals("") || textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals(""))
                {
                    MessageBox.Show("Вы не все ввели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else { 
                database.OpenConnection();

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
                    MessageBox.Show(Convert.ToString(reader["ID_COMPANY"]));
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
                myCommand2.Parameters.AddWithValue("@ID_FLOOR", currentFloor);
                myCommand2.Parameters.AddWithValue("@ID_COMPANY", IDCompany);
                myCommand2.ExecuteNonQuery();
                database.CloseConnection();
                MessageBox.Show("Сведения об офисе были успешно добавлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void breachesDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox8.Image = bitmap;
            pictureBox9.Visible = true;
            pictureBox9.Image = bitmap;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_start_web_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count != 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[listBox1.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    videoSource.Start();
                } else
                {
                    MessageBox.Show("К вашему компьютеру не подключена ни одна вебкамера");
                }
            } catch (Exception exp)
            {
                MessageBox.Show("Во время попытки подключится к вебкамере произошла ошибка!");
            }
            //ОПАСНО написать строку проверки. Если ListBox1 пустой, то вписать текст о несущесвтующих подключениях.
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

            if (start == false) 
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 99;
                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[0].Width = 99;

                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;

                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 0;
                tableLayoutPanel1.ColumnStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
                start = true;
            }
            else
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 45;
                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[0].Width = 33;

                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 45;
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 33;

                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[2].Height = 33;
                tableLayoutPanel1.ColumnStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[2].Width = 33;
                start = false;
            }
        }
    }

}

