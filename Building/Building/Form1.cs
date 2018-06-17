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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Building
{
    //Евгений
    //TODO: При выборе пункта в контекстом меню, которое вызывается нажатием ПКМ по плану этажа, открывается план этажа в другой панели
    //TODO: Спустя 60 дней данные о нарушении должны удаляться 

    //TODO: Подправить скроллинг на главной вкладке
    //TODO: На главной панели стали странно размещаться компоненты. Сейчас это связано с баганутым скроллингом
    //TODO: Добавить у первого этажа камеру, которая будет выводиться на панели камер
    //TODO: Добавить еще одно видео
    //TODO: Добавить оступ слева на главной панели 

    //TODO: Последний столбец у нарушений чтобы был checkBox
    //TODO: При изменении последнего столбца, чтобы изменения сохранялись в БД

    //Захар
    //TODO: Чтобы каждый pictureBox, кроме на главной форме подстраивался правильно под изображения (т.е 16:10, 16:9, 4:3)
    //TODO: Выход с титульника поправить

    //ЗАХАР. ВОт ТуТ прям мастхев
    //TODO: Добавить панель на главной вкладке для увеличенного плана этажа и сверху-справка кнопку для обратного перехода на основную панель
    //TODO: На форме 'Добавить офис' сбилась табуляция. Через код поправь 
    //TODO: Справка
    //TODO: Все же сделай так, чтобы в строку влезало минимум два плана на главной вкладке. Просто сверху в поиске происходит что-то не понятное
    //TODO: На форме добавления офиса у номера офиса не то оформление
    //TODO: Проверь у себя вебкамеру
    //TODO: у TreeView'ере, который находится справа на главной вкладке сбился размер.
    //TODO: Немного увеличь форму по ширине, а т.е задай другой минимум
    //TODO: Пускай нормально открывается главная форма, а т.е постоянно отодвигать панели не очень. Решил на тебя это возложить


    //ЗАХАР! Как думаешь надо ли:
    //TODO: Поменять надписи 'Номер офиса' на 'Офис', т.к у нас еще могут быть буквы (Ц101 хочу писать, типо цокольный этаж)
    //TODO: Поменять с тип этажа 'Подвал' на 'Цокольный этаж'

    public partial class Form1 : Form
    {
        const int INDENT_LEFT = 2;
        const int INDENT_TOP = 25;
        const int INDENT_BETWEEN_PICTURE_BOXES = 10;
        const int INDENT_BETWEEN_LINE_PICTURE_BOXES = 30;
        const int INDENT_WIDTH_LABEL = 85;
        const int INDENT_HEIGHT_LABEL = 150;
        const int WIDTH_PICTURE_BOX = 230;
        const int HEIGHT_PICTURE_BOX = 140;


        int wightPanel;
        int heightPanel;
        Boolean isPanelInfoView = true;
        Boolean isPanelCamerasView = true;
        Boolean isVerticalScrollBeEarly = false;
        List<Label> setLabel;
        List<Label> setShowLabel;
        List<PictureBox> setPictureBox;
        List<PictureBox> setShowPictureBox;
        List<String> setIDFloors;
        List<List<TreeNode>> setTreeNodes; // Необходимо для отображений офисов при выборе конкретного этажа на главной вкладке 
        ObservableCollection<String> collectionForRefresh = new ObservableCollection<String>
        {
            "А"
        };

        BindingSource bindingSourceForBreaches = new BindingSource();
        DataTable dataTableBreaches = new DataTable();
        DataTable dataTableFloors = new DataTable();
        DataTable dataTableOffices = new DataTable();
        DataTable dataTableCameras = new DataTable();
        DataSet ds = new DataSet();
        Database database;

        

        public FilterInfoCollection videoDevices;
        public VideoCaptureDevice videoSource;
        

        private void collectionForRefreshChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add("Офисы");
                    treeView1.Nodes.Add("Камеры");
                    refresh();
                    break;
                case NotifyCollectionChangedAction.Add:
                    refresh();
                    break;
            }
        }

        public void dowloandDateInDateTable()
        {
            dataTableFloors.Clear();
            dataTableOffices.Clear();
            dataTableCameras.Clear();

            string query = "Select * From Floors";
            try
            {

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, database.myConnection))
                {
                    da.Fill(dataTableFloors);
                }

            }
            catch
            {

            }

            query = "Select * From Offices";
            try
            {

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, database.myConnection))
                {
                    da.Fill(dataTableOffices);
                }

            }
            catch
            {

            }

            query = "Select * From Cameras";
            try
            {

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, database.myConnection))
                {
                    da.Fill(dataTableCameras);
                }

            }
            catch
            {

            }

        }


        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(this_MouseWheel);

        }
        public Boolean start = false;
        private void button1_Click(object sender, EventArgs e)
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

        private void скрытьОкноИнформацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (скрытьОкноИнформацииToolStripMenuItem.Checked)
            {
                splitContainer2.Panel2Collapsed = false;
                splitContainer2.Panel2.Show();
                isPanelInfoView = true;
            }
            else
            {
                splitContainer2.Panel2Collapsed = true;
                splitContainer2.Panel2.Hide();
                isPanelInfoView = false;
            }
            alignComponentsFloors();
        }

        private void alignComponentsFloors()
        {
            int XLocationCurrentComponent = 0;
            int YLocationCurrentComponent = 0;
            wightPanel = this.splitContainer3.Panel2.Width;
            heightPanel = this.splitContainer3.Panel2.Height;
            
            foreach (PictureBox currentPictureBox in setShowPictureBox)
            {
                if (XLocationCurrentComponent + WIDTH_PICTURE_BOX + 15 > wightPanel)
                {
                    XLocationCurrentComponent = 0;
                    YLocationCurrentComponent += INDENT_TOP + INDENT_HEIGHT_LABEL + INDENT_BETWEEN_LINE_PICTURE_BOXES;
                }

                currentPictureBox.Location = new Point(INDENT_LEFT + XLocationCurrentComponent, INDENT_TOP + YLocationCurrentComponent);
                //labelFloor.Location = new Point(INDENT_LEFT + INDENT_WIDTH_LABEL + XLocationCurrentComponent, INDENT_TOP + INDENT_HEIGHT_LABEL + YLocationCurrentComponent);

                currentPictureBox.Visible = true;

                XLocationCurrentComponent += WIDTH_PICTURE_BOX + INDENT_BETWEEN_PICTURE_BOXES;
            }

            XLocationCurrentComponent = 0;
            YLocationCurrentComponent = 0;

            foreach (Label currentLabel in setShowLabel)
            {
                if (XLocationCurrentComponent + WIDTH_PICTURE_BOX + 15 > wightPanel)
                {
                    XLocationCurrentComponent = 0;
                    YLocationCurrentComponent += INDENT_TOP + INDENT_HEIGHT_LABEL + INDENT_BETWEEN_LINE_PICTURE_BOXES;
                }

                currentLabel.Location = new Point(INDENT_LEFT + INDENT_WIDTH_LABEL + XLocationCurrentComponent, INDENT_TOP + INDENT_HEIGHT_LABEL + YLocationCurrentComponent);

                //labelFloor.Location = new Point(INDENT_LEFT + INDENT_WIDTH_LABEL + XLocationCurrentComponent, INDENT_TOP + INDENT_HEIGHT_LABEL + YLocationCurrentComponent);

                currentLabel.Visible = true;

                XLocationCurrentComponent += WIDTH_PICTURE_BOX + INDENT_BETWEEN_PICTURE_BOXES;
            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            Form1 firstForm = new Form1();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }


        private void панельКамерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (панельКамерToolStripMenuItem.Checked)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                isPanelCamerasView = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
                isPanelCamerasView = false;
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
            if (isPanelInfoView)
            {
                splitContainer2.Panel2Collapsed = false;
                splitContainer2.Panel2.Show();
                скрытьОкноИнформацииToolStripMenuItem.Checked = true;
            }
            else
            {
                splitContainer2.Panel2Collapsed = true;
                splitContainer2.Panel2.Hide();
                скрытьОкноИнформацииToolStripMenuItem.Checked = false;
            }
            if (isPanelCamerasView)
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                панельКамерToolStripMenuItem.Checked = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
                панельКамерToolStripMenuItem.Checked = false;
            }
            alignComponentsFloors();
        }

        List<String> setWebCameras = new List<String>();
        private void Form1_Load(object sender, EventArgs e)
        {
            Form7 titulForm = new Form7();
            titulForm.ShowInTaskbar = false;                           //скрыть вторую форму из панели задач   
            titulForm.ShowDialog();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "buildingNewDataSet.Breaches". При необходимости она может быть перемещена или удалена.
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    setWebCameras.Add(device.Name);
                }


            }
            //Необходимо для относительного пути к базе данных
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            treeView1.Nodes.Add("Офисы");
            treeView1.Nodes.Add("Камеры");

            //Убираем панели контроля видеоряда 
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer2.uiMode = "none";
            axWindowsMediaPlayer3.uiMode = "none";
            axWindowsMediaPlayer4.uiMode = "none";
            axWindowsMediaPlayer5.uiMode = "none";
            axWindowsMediaPlayer6.uiMode = "none";

            axWindowsMediaPlayer1.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera1.mp4";
            axWindowsMediaPlayer2.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera2.mp4";
            axWindowsMediaPlayer3.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera3.mp4";
            axWindowsMediaPlayer4.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera4.mp4";
            axWindowsMediaPlayer5.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera5.mp4";
            axWindowsMediaPlayer6.URL = Directory.GetCurrentDirectory() + "/Resources/video/camera5.mp4";

            axWindowsMediaPlayer1.settings.mute = true;
            axWindowsMediaPlayer2.settings.mute = true;
            axWindowsMediaPlayer3.settings.mute = true;
            axWindowsMediaPlayer4.settings.mute = true;
            axWindowsMediaPlayer5.settings.mute = true;
            axWindowsMediaPlayer6.settings.mute = true;

            axWindowsMediaPlayer1.settings.playCount = 1000;
            axWindowsMediaPlayer2.settings.playCount = 1000;
            axWindowsMediaPlayer3.settings.playCount = 1000;
            axWindowsMediaPlayer4.settings.playCount = 1000;
            axWindowsMediaPlayer5.settings.playCount = 1000;
            axWindowsMediaPlayer6.settings.playCount = 1000;

            axWindowsMediaPlayer6.stretchToFit = false;



            this.splitContainer3.Panel2.HorizontalScroll.Enabled = false;
            this.splitContainer3.Panel2.HorizontalScroll.Visible = false;
            this.splitContainer3.Panel2.HorizontalScroll.Maximum = 0;
            this.splitContainer3.Panel2.AutoScroll = true;

            collectionForRefresh.CollectionChanged += collectionForRefreshChanged;


            database = new Database();
            database.OpenConnection();

            dowloandDateInDateTable();

            //Таблица "Нарушения"
            database.OpenConnection();
            string query = "Select * From Breaches";
            try
            {

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, database.myConnection))
                {
                    da.Fill(dataTableBreaches);
                    breachesDataGridView.DataSource = dataTableBreaches;
                }

            }
            catch
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

            setLabel = new List<Label>();
            setPictureBox = new List<PictureBox>();
            setShowLabel = new List<Label>();
            setShowPictureBox = new List<PictureBox>();
            setIDFloors = new List<string>();

            int XLocationCurrentComponent = 0;
            int YLocationCurrentComponent = 0;
            String errorMessageDownloadImagesStr = null;

            wightPanel = this.splitContainer3.Panel2.Width;
            heightPanel = this.splitContainer3.Panel2.Height;

            string queryDataFloor = "SELECT * FROM Floors";
            SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
            myCommandDataFloor.CommandText = queryDataFloor;
            myCommandDataFloor.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();

            while (reader.Read())
            {
                setIDFloors.Add(Convert.ToString(reader["ID_FLOOR"]));

                Label labelFloor = new Label();
                labelFloor.Text = Convert.ToString(reader["CATEGORY_FLOOR"]) + " " + Convert.ToString(reader["ID_FLOOR"]);
                labelFloor.Font = new Font("Arial", 12);
                PictureBox pictureBox = new PictureBox();
                pictureBox.Tag = Convert.ToString(reader["PATH"]) + "?" + Convert.ToString(reader["ID_FLOOR"]);
                pictureBox.MouseClick += new MouseEventHandler(PictureBoxClick);

                pictureBox.Width = WIDTH_PICTURE_BOX;
                pictureBox.Height = HEIGHT_PICTURE_BOX;

                try
                {
                    //Если абсолютный путь
                    pictureBox.Load(Convert.ToString(reader["PATH"]));
                } catch
                {
                    try
                    {
                        //Если относительный путь 
                        pictureBox.Load(Directory.GetCurrentDirectory() + Convert.ToString(reader["PATH"]));
                    }
                    catch { 
                        errorMessageDownloadImagesStr +=  "\n" + labelFloor.Text;
                        //TODO: pictureBox.Load(); Загрузка изображения "Ошибка при загрузке плана"
                    }

                }
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;




                if (XLocationCurrentComponent + WIDTH_PICTURE_BOX > wightPanel)
                {
                    XLocationCurrentComponent = 0;
                    YLocationCurrentComponent += INDENT_TOP + INDENT_HEIGHT_LABEL + INDENT_BETWEEN_LINE_PICTURE_BOXES;
                }

                pictureBox.Location = new Point(INDENT_LEFT + XLocationCurrentComponent, INDENT_TOP + YLocationCurrentComponent);
                labelFloor.Location = new Point(INDENT_LEFT + INDENT_WIDTH_LABEL + XLocationCurrentComponent, INDENT_TOP + INDENT_HEIGHT_LABEL + YLocationCurrentComponent);


                XLocationCurrentComponent += WIDTH_PICTURE_BOX + INDENT_BETWEEN_PICTURE_BOXES;

                setLabel.Add(labelFloor);
                setShowLabel.Add(labelFloor);
                setPictureBox.Add(pictureBox);
                setShowPictureBox.Add(pictureBox);

                //Добавляем элементы на форму
                this.splitContainer3.Panel2.Controls.Add(setPictureBox.Last());
                this.splitContainer3.Panel2.Controls.Add(setLabel.Last());
            }

            database.CloseConnection();
            if (errorMessageDownloadImagesStr != null)
            {
                MessageBox.Show("Проблемы с путем имеют следующие этажи:" + errorMessageDownloadImagesStr, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PictureBoxClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add("Увеличить размер плана");
                contextMenu.Show(this, new Point(e.X + ((Control)sender).Left, e.Y + ((Control)sender).Top + 35));
            }
            else
            {
                foreach(var pictureBox in setPictureBox)
                {
                    pictureBox.BorderStyle = BorderStyle.None;
                }
                treeView1.Nodes.Clear();
                (sender as PictureBox).BorderStyle = BorderStyle.FixedSingle;

                database.OpenConnection();

                string[] words = Convert.ToString((sender as PictureBox).Tag).Split('?');
                string idFloor = words[1];
                string queryDataOffices = "SELECT * FROM Offices WHERE ID_FLOOR = " + idFloor;
                SQLiteCommand myCommandDataOffices = database.myConnection.CreateCommand();
                myCommandDataOffices.CommandText = queryDataOffices;
                myCommandDataOffices.CommandType = CommandType.Text;
                SQLiteDataReader readerDataOffices = myCommandDataOffices.ExecuteReader();

                TreeNode treeNodeTitleNameCompany;
                TreeNode treeNodeTitleDescription;
                TreeNode treeNodeTitlePhoneCompany;

                TreeNode treeNodeOffice;
                TreeNode treeNodeNameCompany;
                TreeNode treeNodeDescription;
                TreeNode treeNodePhone;

                TreeNode treeNodeTitleOffices;
                treeNodeTitleOffices = new TreeNode("Офисы");
                treeView1.Nodes.Add(treeNodeTitleOffices);

                List<TreeNode> setIDOffices = new List<TreeNode>();
                string idCompany = null;
                int numberOffice = 0;
                while (readerDataOffices.Read())
                {
                    treeNodeTitleNameCompany = new TreeNode("Название организации");
                    treeNodeTitleDescription = new TreeNode("Описание деятельности");
                    treeNodeTitlePhoneCompany = new TreeNode("Контактный телефон");
                    treeNodeOffice = new TreeNode("Офис " + Convert.ToString(readerDataOffices["ID_OFFICE"]));
                    setIDOffices.Add(treeNodeOffice);
                    //treeView1.Nodes.Add(treeNodeOffice);

                    idCompany = Convert.ToString(readerDataOffices["ID_COMPANY"]);

                    // Извлекаем данные о компании
                    string queryDataCompany = "SELECT * FROM Companies WHERE ID_COMPANY = " + idCompany;
                    SQLiteCommand myCommandDataCompany = database.myConnection.CreateCommand();
                    myCommandDataCompany.CommandText = queryDataCompany;
                    myCommandDataCompany.CommandType = CommandType.Text;
                    SQLiteDataReader readerCompany = myCommandDataCompany.ExecuteReader();
                    treeView1.Nodes[0].Nodes.Add(treeNodeOffice);
                    while (readerCompany.Read())
                    {
                        treeNodeNameCompany = new TreeNode(Convert.ToString(readerCompany["NAME_COMPANY"]));
                        treeNodeDescription = new TreeNode(Convert.ToString(readerCompany["DESCRIPTION"]));
                        treeNodePhone = new TreeNode(Convert.ToString(readerCompany["PHONE_COMPANY"]));

                        treeView1.Nodes[0].Nodes[numberOffice].Nodes.Add(treeNodeTitleNameCompany);
                        treeView1.Nodes[0].Nodes[numberOffice].Nodes.Add(treeNodeTitleDescription);
                        treeView1.Nodes[0].Nodes[numberOffice].Nodes.Add(treeNodeTitlePhoneCompany);

                        treeView1.Nodes[0].Nodes[numberOffice].Nodes[0].Nodes.Add(treeNodeNameCompany);
                        treeView1.Nodes[0].Nodes[numberOffice].Nodes[1].Nodes.Add(treeNodeDescription);
                        treeView1.Nodes[0].Nodes[numberOffice].Nodes[2].Nodes.Add(treeNodePhone);
                        numberOffice++;
                    }
                }

                string queryDataCameras = "SELECT * FROM Cameras WHERE ID_FLOOR = " + idFloor;
                SQLiteCommand myCommandDataCameras = database.myConnection.CreateCommand();
                myCommandDataCameras.CommandText = queryDataCameras;
                myCommandDataCameras.CommandType = CommandType.Text;
                SQLiteDataReader readerDataCameras = myCommandDataCameras.ExecuteReader();

                TreeNode treeNodeCamera;
                TreeNode treeNodesDescription;
                TreeNode treeNodeTitleCameras;
                treeNodeTitleCameras = new TreeNode("Камеры");
                treeView1.Nodes.Add(treeNodeTitleCameras);
                int numberCamera = 0;
                while (readerDataCameras.Read())
                {
                    treeNodeCamera = new TreeNode(Convert.ToString(readerDataCameras["IP_CAMERA"]));
                    treeNodeDescription = new TreeNode(Convert.ToString(readerDataCameras["DESCRIPTION"]));
                    treeView1.Nodes[1].Nodes.Add(treeNodeCamera);
                    treeView1.Nodes[1].Nodes[numberCamera].Nodes.Add(treeNodeDescription);
                    numberCamera++;
                }


                database.CloseConnection();

                if (idFloor == "1")
                {
                    axWindowsMediaPlayer6.Visible = true;
                    label12.Visible = true;
                } else
                {
                    axWindowsMediaPlayer6.Visible = false;
                    label12.Visible = false;
                }
            }
        }

        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (main_panel.Visible)
            {

                if (e.Delta > 0)
                {
                    if (this.splitContainer3.Panel2.VerticalScroll.Value - 10  >= this.splitContainer3.Panel2.VerticalScroll.Minimum)
                    {
                        this.splitContainer3.Panel2.VerticalScroll.Value -= 10;
                    } else
                    {
                        this.splitContainer3.Panel2.VerticalScroll.Value = this.splitContainer3.Panel2.VerticalScroll.Minimum;
                    }
                }
                else
                {
                    if (this.splitContainer3.Panel2.VerticalScroll.Value + 10 <= this.splitContainer3.Panel2.VerticalScroll.Maximum)
                    {
                        this.splitContainer3.Panel2.VerticalScroll.Value += 10;
                    } else
                    {
                        this.splitContainer3.Panel2.VerticalScroll.Value = this.splitContainer3.Panel2.VerticalScroll.Maximum;
                    }
                }
            }
        }
        private void breachesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.breachesBindingSource.EndEdit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2(dataTableBreaches, dataTableFloors);
            secondForm.ShowInTaskbar = false;                           //скрыть вторую форму из панели задач   
            secondForm.ShowDialog();
        }

        private void камерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (setWebCameras.Count != 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                   // MessageBox.Show(videoSource);
                    videoSource.Start();
                }
                else
                {
                    MessageBox.Show("К вашему компьютеру не подключена ни одна вебкамера");
                }
            }
            catch
            {
                MessageBox.Show("Во время попытки подключиться к вебкамере произошла ошибка!");
            }

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
            pictureBox1.Tag = null;
            pictureBox1.Invalidate();
            label14.Visible = true;
            button7.Visible = false;
        }

        private void камерыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form6 sixForm = new Form6("Редактирование", collectionForRefresh, dataTableFloors, dataTableCameras);
            sixForm.Text = "Редактирование информации о камере";
            sixForm.ShowInTaskbar = false;                           //Открытие 6-ой формы "Редактирование информации о камере"   
            sixForm.ShowDialog();
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
                database.OpenConnection();

                //Проверка на наличие этажа
                string queryCheckExist = "SELECT ID_FLOOR FROM Floors WHERE ID_FLOOR =  " + textBox2.Text;
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
                    MessageBox.Show("Данный этаж существует. Добавление невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Проверка на наличие крыши
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

                    if (isExist)
                    {
                        MessageBox.Show("Информация о крыше уже есть. Невозможно добавить вторую", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        label21.Text = "Офис находится на этаже " + textBox2.Text;
                        label2.Text = "Камера находится на этаже " + textBox2.Text;

                        // Таблица "Этажи"
                        string query = "INSERT INTO Floors ('ID_FLOOR', 'CATEGORY_FLOOR', 'PATH') VALUES (@ID_FLOOR, @CATEGORY_FLOOR, @PATH) ";
                        SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                        myCommand.Parameters.AddWithValue("@ID_FLOOR", textBox2.Text);
                        myCommand.Parameters.AddWithValue("@CATEGORY_FLOOR", comboBox2.Text);
                        myCommand.Parameters.AddWithValue("@PATH", pictureBox1.Tag);
                        myCommand.ExecuteNonQuery();
                        database.CloseConnection();
                        currentFloor = textBox2.Text;
                        MessageBox.Show("Сведения об этаже были успешно добавлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        DataRow row = dataTableFloors.NewRow();
                        row[0] = textBox2.Text;
                        row[1] = comboBox2.Text;
                        row[2] = pictureBox1.Tag;
                        dataTableFloors.Rows.Add(row);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openDialog();
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

                    ofd.InitialDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());

                    //Сохранение пути изображения в свойстве компонента PictureBox. Причем если изображение находится в папке программы, то сохраняется относительный путь
                    pictureBox1.Tag = ofd.FileName.Replace(Directory.GetCurrentDirectory(), "");
                   // MessageBox.Show(ofd.FileName.Replace(Directory.GetCurrentDirectory(), ""));
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
            Form3 thirdForm = new Form3("Редактирование", Directory.GetCurrentDirectory(), collectionForRefresh, dataTableFloors);
            thirdForm.ShowInTaskbar = false;                                        //скрыть вторую форму из панели задач
            thirdForm.ShowDialog();
            thirdForm.Text = "Редактирование информации об этаже";
        }

        private void офисыToolStripMenuItem_Click(object sender, EventArgs e)       //Вызов формы "Редактировать офис"
        {
            Form4 fourForm = new Form4("Редактирование", collectionForRefresh, dataTableFloors, dataTableOffices);
            fourForm.Text = "Редактирование информации об офисе";
            fourForm.ShowInTaskbar = false;                           //Открытие 4-ой формы "Редактирование информации об офисах"   
            fourForm.ShowDialog();
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
                else
                {
                    database.OpenConnection();

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
                        myCommand2.Parameters.AddWithValue("@ID_FLOOR", currentFloor);
                        myCommand2.Parameters.AddWithValue("@ID_COMPANY", IDCompany);
                        myCommand2.ExecuteNonQuery();
                        database.CloseConnection();
                        MessageBox.Show("Сведения об офисе были успешно добавлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 16)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 45 && textBox2.Text == "")
            {
                e.Handled = false;
            }
        }


        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox8.Image = bitmap;

        }

        private void button_start_web_Click(object sender, EventArgs e)
        {

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

        private void добавитьИнформациюОбОфисеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fourForm = new Form4("Добавление", collectionForRefresh, dataTableFloors, dataTableOffices);
            fourForm.Text = "Добавление информацию об офисе";
            fourForm.ShowInTaskbar = false;                           //Открытие 4-ой формы "Добавление информации об офисах"   
            fourForm.ShowDialog();
        }

        private void этажToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form5 fiveForm = new Form5("Этаж", collectionForRefresh, dataTableFloors, dataTableOffices, dataTableCameras);
            fiveForm.Text = "Удаление информации об этаже";
            fiveForm.ShowInTaskbar = false;                           //Открытие 5-ой формы "Удаление этажа"   
            fiveForm.ShowDialog();
        }

        private void офисToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 fiveForm = new Form5("Офис", collectionForRefresh, null, dataTableOffices, null);
            fiveForm.Text = "Удаление информации об офисе";
            fiveForm.ShowInTaskbar = false;                           //Открытие 5-ой формы "Удаление офиса"   
            fiveForm.ShowDialog();
        }

        private void камераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 fiveForm = new Form5("Камера", collectionForRefresh, null, null, dataTableCameras);
            fiveForm.Text = "Удаление информации о камере";
            fiveForm.ShowInTaskbar = false;                           //Открытие 5-ой формы "Удаление камеры"   
            fiveForm.ShowDialog();
        }

        private void гToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fourForm = new Form4("Добавление", collectionForRefresh,dataTableFloors, dataTableOffices);
            fourForm.Text = "Добавление информации об офисе";
            fourForm.ShowInTaskbar = false;                           //Открытие 4-ой формы "Добавление информации об офисах"   
            fourForm.ShowDialog();
        }

        private void информацияОКамереToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 sixForm = new Form6("Добавление", collectionForRefresh, dataTableFloors, dataTableCameras);
            sixForm.Text = "Добавление информации о камере";
            sixForm.ShowInTaskbar = false;                           //Открытие 6-ой формы "Добавление информации о камере"   
            sixForm.ShowDialog();
        }

        private void информацияОбЭтажеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 thirdForm = new Form3("Добавление", collectionForRefresh, dataTableFloors);
            thirdForm.Text = "Добавление информации об этаже";
            thirdForm.ShowInTaskbar = false;                                        //скрыть вторую форму из панели задач
            thirdForm.ShowDialog();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            alignComponentsFloors();
            if (splitContainer3.Panel2.VerticalScroll.Visible)
            {
                if (isVerticalScrollBeEarly == false)
                {
                    button2.Location = new Point(button2.Location.X - 15, button2.Location.Y);
                    //button2.Location = new Point(splitContainer3.Panel2.Width - 20, button2.Location.Y);
                    isVerticalScrollBeEarly = true;
                }
            } else
            {
                if (isVerticalScrollBeEarly == true)
                {
                    button2.Location = new Point(button2.Location.X + 15, button2.Location.Y);
                    //button2.Location = new Point(splitContainer3.Panel2.Width - 5, button2.Location.Y);
                    isVerticalScrollBeEarly = false;
                }
            }
        }

        private void редактироватьЭтажToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Вы ничего не ввели!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                database.OpenConnection();
                foreach (var pictureBox in setPictureBox)
                {
                    pictureBox.Visible = false;
                }
                foreach (var label in setLabel)
                {
                    label.Visible = false;
                }
                switch (comboBox1.Text)
                {
                    case "Номер этажа":
                        if (setIDFloors.Contains(textBox1.Text))
                        {
                            setShowLabel.Clear();
                            setShowPictureBox.Clear();
                            setShowPictureBox.Add(setPictureBox[setIDFloors.IndexOf(textBox1.Text)]);
                            setShowLabel.Add(setLabel[setIDFloors.IndexOf(textBox1.Text)]);
                            button2.Visible = true;
                        } else
                        {
                            MessageBox.Show("Ничего не было найдено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case "Номер офиса":
                        string queryDataOffices = "SELECT * FROM Offices WHERE ID_OFFICE = " + textBox1.Text;
                        SQLiteCommand myCommandDataOffices = database.myConnection.CreateCommand();
                        myCommandDataOffices.CommandText = queryDataOffices;
                        myCommandDataOffices.CommandType = CommandType.Text;
                        SQLiteDataReader readerDataOffices = myCommandDataOffices.ExecuteReader();
                        if(readerDataOffices.StepCount == 0)
                        {
                            MessageBox.Show("Ничего не было найдено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        } else
                        {
                            setShowLabel.Clear();
                            setShowPictureBox.Clear();
                            button2.Visible = true;
                        }
                        while (readerDataOffices.Read())
                        {
                            setShowPictureBox.Add(setPictureBox[setIDFloors.IndexOf(Convert.ToString(readerDataOffices["ID_FLOOR"]))]);
                            setShowLabel.Add(setLabel[setIDFloors.IndexOf(Convert.ToString(readerDataOffices["ID_FLOOR"]))]);
                        }
                        break;
                    case "Тип этажа":
                        string queryDataFloor = "SELECT * FROM Floors WHERE CATEGORY_FLOOR = '" + textBox1.Text + "'";
                        SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
                        myCommandDataFloor.CommandText = queryDataFloor;
                        myCommandDataFloor.CommandType = CommandType.Text;
                        SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();
                        if (reader.StepCount == 0)
                        {
                            MessageBox.Show("Ничего не было найдено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            setShowLabel.Clear();
                            setShowPictureBox.Clear();
                            button2.Visible = true;
                        }
                        while (reader.Read())
                        {
                            setShowPictureBox.Add(setPictureBox[setIDFloors.IndexOf(Convert.ToString(reader["ID_FLOOR"]))]);
                            setShowLabel.Add(setLabel[setIDFloors.IndexOf(Convert.ToString(reader["ID_FLOOR"]))]);
                        }
                        break;

                }

                database.CloseConnection();
                alignComponentsFloors();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setShowLabel.Clear();
            setShowPictureBox.Clear();

            foreach (var pictureBox in setPictureBox)
            {
                setShowPictureBox.Add(pictureBox);
            }

            foreach (var label in setLabel)
            {
                setShowLabel.Add(label);
            }
            button2.Visible = false;
            alignComponentsFloors();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (collectionForRefresh != null)
            {
                MessageBox.Show(Convert.ToString(collectionForRefresh.Count));
            }
            else
            {

              
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentFloor == null)
            {
                MessageBox.Show("Вначале необходимо добавить сведения об этаже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox7.Text.Equals("") || textBox8.Text.Equals("") || textBox9.Text.Equals(""))
                {
                    MessageBox.Show("Вы не все ввели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    database.OpenConnection();

                    //Проверка на наличие камеры
                    string queryCheckExist = "SELECT IP_CAMERA FROM Cameras WHERE IP_CAMERA =  " + textBox8.Text;
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
                        string queryIDCompany = "SELECT ID_CAMERA FROM Cameras ORDER BY ID_COMPANY DESC LIMIT 1";
                        SQLiteCommand myCommandIDCompany = database.myConnection.CreateCommand();
                        myCommandIDCompany.CommandText = queryIDCompany;
                        myCommandIDCompany.CommandType = CommandType.Text;
                        SQLiteDataReader reader = myCommandIDCompany.ExecuteReader();
                        int IDCamera = 0;
                        while (reader.Read())
                        {
                            IDCamera = Convert.ToInt16(Convert.ToString(reader["ID_CAMERA"])) + 1;
                        }

                        // Таблица "Камеры"
                        string query = "INSERT INTO Cameras ('ID_CAMERA','IP_CAMERA', 'MAC_CAMERA', 'DESCRIPTION') VALUES (@ID_CAMERA, @IP_CAMERA, @MAC_CAMERA, @DESCRIPTION) ";
                        SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                        myCommand.Parameters.AddWithValue("@ID_CAMERA", IDCamera);
                        myCommand.Parameters.AddWithValue("@IP_CAMERA", textBox8.Text);
                        myCommand.Parameters.AddWithValue("@MAC_CAMERA", textBox7.Text);
                        myCommand.Parameters.AddWithValue("@DESCRIPTION", textBox9.Text);
                        myCommand.ExecuteNonQuery();

                    }

                    database.CloseConnection();
                }
            }
        }

        private void спарвкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //          (@".\Manual.pdf");                               //попробовать сокрощение пути
            System.Diagnostics.Process.Start("C:/Users/Ironik/Desktop/w.pdf");               //Открвает конкретный файл

        }

        private void refresh()
        {
            setShowLabel.Clear();
            setShowPictureBox.Clear();

            database.OpenConnection();

            string queryDataFloor = "SELECT * FROM Floors";
            SQLiteCommand myCommandDataFloor = database.myConnection.CreateCommand();
            myCommandDataFloor.CommandText = queryDataFloor;
            myCommandDataFloor.CommandType = CommandType.Text;
            SQLiteDataReader reader = myCommandDataFloor.ExecuteReader();


            int XLocationCurrentComponent = 0;
            int YLocationCurrentComponent = 0;
            String errorMessageDownloadImagesStr = null;

            setIDFloors.Clear();

            foreach (var pictureBox in setPictureBox)
            {
                pictureBox.Dispose();
            }
            setPictureBox.Clear();

            foreach (var label in setLabel)
            {
                label.Dispose();
            }
            setLabel.Clear();

            while (reader.Read())
            {
                //if()
                //Если не содержит
                // if (!setIDFloors.Contains(Convert.ToString(reader["ID_FLOOR"]))){
                setIDFloors.Add(Convert.ToString(reader["ID_FLOOR"]));

                Label labelFloor = new Label();
                labelFloor.Text = Convert.ToString(reader["CATEGORY_FLOOR"]) + " " + Convert.ToString(reader["ID_FLOOR"]);
                labelFloor.Font = new Font("Arial", 12);
                PictureBox pictureBox = new PictureBox();
                pictureBox.Tag = Convert.ToString(reader["PATH"]) + "?" + Convert.ToString(reader["ID_FLOOR"]);
                pictureBox.MouseClick += new MouseEventHandler(PictureBoxClick);

                pictureBox.Width = WIDTH_PICTURE_BOX;
                pictureBox.Height = HEIGHT_PICTURE_BOX;

                try
                {
                    //Если абсолютный путь
                    pictureBox.Load(Convert.ToString(reader["PATH"]));
                }
                catch
                {
                    try
                    {
                        //Если относительный путь 
                        pictureBox.Load(Directory.GetCurrentDirectory() + Convert.ToString(reader["PATH"]));
                    }
                    catch
                    {
                        errorMessageDownloadImagesStr += "\n" + labelFloor.Text;
                        //TODO: pictureBox.Load(); Загрузка изображения "Ошибка при загрузке плана"
                    }

                }
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;




                if (XLocationCurrentComponent + WIDTH_PICTURE_BOX > wightPanel)
                {
                    XLocationCurrentComponent = 0;
                    YLocationCurrentComponent += INDENT_TOP + INDENT_HEIGHT_LABEL + INDENT_BETWEEN_LINE_PICTURE_BOXES;
                }

                pictureBox.Location = new Point(INDENT_LEFT + XLocationCurrentComponent, INDENT_TOP + YLocationCurrentComponent);
                labelFloor.Location = new Point(INDENT_LEFT + INDENT_WIDTH_LABEL + XLocationCurrentComponent, INDENT_TOP + INDENT_HEIGHT_LABEL + YLocationCurrentComponent);


                XLocationCurrentComponent += WIDTH_PICTURE_BOX + INDENT_BETWEEN_PICTURE_BOXES;

                setLabel.Add(labelFloor);
                setShowLabel.Add(labelFloor);
                setPictureBox.Add(pictureBox);
                setShowPictureBox.Add(pictureBox);
                //Добавляем элементы на форму
                this.splitContainer3.Panel2.Controls.Add(setPictureBox.Last());
                this.splitContainer3.Panel2.Controls.Add(setLabel.Last());
            }

            //}



            database.CloseConnection();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (setPictureBox != null)
            {
                alignComponentsFloors();
            }
        }
    }



}

