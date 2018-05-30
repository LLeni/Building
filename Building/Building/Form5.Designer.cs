namespace Building
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.floorsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.buldingDataSet2 = new Building.BuldingDataSet2();
            this.floorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildingDataSet = new Building.BuildingDataSet();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.officesBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.officesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.camerasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.camerasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.breachesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.breachesTableAdapter = new Building.BuildingDataSetTableAdapters.BreachesTableAdapter();
            this.floorsTableAdapter = new Building.BuildingDataSetTableAdapters.FloorsTableAdapter();
            this.officesTableAdapter = new Building.BuildingDataSetTableAdapters.OfficesTableAdapter();
            this.camerasTableAdapter = new Building.BuildingDataSetTableAdapters.CamerasTableAdapter();
            this.floorsTableAdapter1 = new Building.BuldingDataSet2TableAdapters.FloorsTableAdapter();
            this.buldingDataSet2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.officesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.officesTableAdapter1 = new Building.BuldingDataSet2TableAdapters.OfficesTableAdapter();
            this.camerasTableAdapter1 = new Building.BuldingDataSet2TableAdapters.CamerasTableAdapter();
            this.officesBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.buldingDataSet2BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.floorsBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camerasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camerasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.breachesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(62, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Удалить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(262, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(74, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(103, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 23);
            this.label2.TabIndex = 24;
            this.label2.Text = "Удаление информации об этаже";
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.floorsBindingSource2;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(200, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(194, 27);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // floorsBindingSource1
            // 
            this.floorsBindingSource1.DataMember = "Floors";
            this.floorsBindingSource1.DataSource = this.buldingDataSet2;
            // 
            // buldingDataSet2
            // 
            this.buldingDataSet2.DataSetName = "BuldingDataSet2";
            this.buldingDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // floorsBindingSource
            // 
            this.floorsBindingSource.DataMember = "Floors";
            this.floorsBindingSource.DataSource = this.buildingDataSet;
            // 
            // buildingDataSet
            // 
            this.buildingDataSet.DataSetName = "BuildingDataSet";
            this.buildingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBox2
            // 
            this.comboBox2.DataSource = this.officesBindingSource3;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(200, 55);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(194, 27);
            this.comboBox2.TabIndex = 26;
            // 
            // officesBindingSource3
            // 
            this.officesBindingSource3.DataMember = "Offices";
            this.officesBindingSource3.DataSource = this.buldingDataSet2;
            // 
            // officesBindingSource
            // 
            this.officesBindingSource.DataMember = "Offices";
            this.officesBindingSource.DataSource = this.buildingDataSet;
            // 
            // comboBox3
            // 
            this.comboBox3.DataSource = this.camerasBindingSource1;
            this.comboBox3.DisplayMember = "ID_CAMERA";
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(200, 55);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(194, 27);
            this.comboBox3.TabIndex = 27;
            // 
            // camerasBindingSource1
            // 
            this.camerasBindingSource1.DataMember = "Cameras";
            this.camerasBindingSource1.DataSource = this.buldingDataSet2;
            // 
            // camerasBindingSource
            // 
            this.camerasBindingSource.DataMember = "Cameras";
            this.camerasBindingSource.DataSource = this.buildingDataSet;
            // 
            // breachesBindingSource
            // 
            this.breachesBindingSource.DataMember = "Breaches";
            this.breachesBindingSource.DataSource = this.buildingDataSet;
            // 
            // breachesTableAdapter
            // 
            this.breachesTableAdapter.ClearBeforeFill = true;
            // 
            // floorsTableAdapter
            // 
            this.floorsTableAdapter.ClearBeforeFill = true;
            // 
            // officesTableAdapter
            // 
            this.officesTableAdapter.ClearBeforeFill = true;
            // 
            // camerasTableAdapter
            // 
            this.camerasTableAdapter.ClearBeforeFill = true;
            // 
            // floorsTableAdapter1
            // 
            this.floorsTableAdapter1.ClearBeforeFill = true;
            // 
            // buldingDataSet2BindingSource
            // 
            this.buldingDataSet2BindingSource.DataSource = this.buldingDataSet2;
            this.buldingDataSet2BindingSource.Position = 0;
            // 
            // officesBindingSource1
            // 
            this.officesBindingSource1.DataMember = "Offices";
            this.officesBindingSource1.DataSource = this.buldingDataSet2BindingSource;
            // 
            // officesTableAdapter1
            // 
            this.officesTableAdapter1.ClearBeforeFill = true;
            // 
            // camerasTableAdapter1
            // 
            this.camerasTableAdapter1.ClearBeforeFill = true;
            // 
            // officesBindingSource2
            // 
            this.officesBindingSource2.DataMember = "Offices";
            this.officesBindingSource2.DataSource = this.buldingDataSet2;
            // 
            // buldingDataSet2BindingSource1
            // 
            this.buldingDataSet2BindingSource1.DataSource = this.buldingDataSet2;
            this.buldingDataSet2BindingSource1.Position = 0;
            // 
            // floorsBindingSource2
            // 
            this.floorsBindingSource2.DataMember = "Floors";
            this.floorsBindingSource2.DataSource = this.buldingDataSet2;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 151);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camerasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camerasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.breachesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officesBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buldingDataSet2BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private BuildingDataSet buildingDataSet;
        private System.Windows.Forms.BindingSource breachesBindingSource;
        private BuildingDataSetTableAdapters.BreachesTableAdapter breachesTableAdapter;
        private System.Windows.Forms.BindingSource floorsBindingSource;
        private BuildingDataSetTableAdapters.FloorsTableAdapter floorsTableAdapter;
        private System.Windows.Forms.BindingSource officesBindingSource;
        private BuildingDataSetTableAdapters.OfficesTableAdapter officesTableAdapter;
        private System.Windows.Forms.BindingSource camerasBindingSource;
        private BuildingDataSetTableAdapters.CamerasTableAdapter camerasTableAdapter;
        private BuldingDataSet2 buldingDataSet2;
        private System.Windows.Forms.BindingSource floorsBindingSource1;
        private BuldingDataSet2TableAdapters.FloorsTableAdapter floorsTableAdapter1;
        private System.Windows.Forms.BindingSource buldingDataSet2BindingSource;
        private System.Windows.Forms.BindingSource officesBindingSource1;
        private BuldingDataSet2TableAdapters.OfficesTableAdapter officesTableAdapter1;
        private System.Windows.Forms.BindingSource camerasBindingSource1;
        private BuldingDataSet2TableAdapters.CamerasTableAdapter camerasTableAdapter1;
        private System.Windows.Forms.BindingSource officesBindingSource2;
        private System.Windows.Forms.BindingSource officesBindingSource3;
        private System.Windows.Forms.BindingSource floorsBindingSource2;
        private System.Windows.Forms.BindingSource buldingDataSet2BindingSource1;
    }
}