﻿namespace Building
{
    partial class Form2
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
            System.Windows.Forms.Label iD_FLOORLabel;
            System.Windows.Forms.Label lOCATION_BREACHLabel;
            System.Windows.Forms.Label tOPIC_BREACHLabel;
            System.Windows.Forms.Label dESCRIPTION_BREACHLabel;
            System.Windows.Forms.Label dATE_BREACHLabel;
            this.lOCATION_BREACHTextBox = new System.Windows.Forms.TextBox();
            this.tOPIC_BREACHTextBox = new System.Windows.Forms.TextBox();
            this.dATE_BREACHDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.floorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            iD_FLOORLabel = new System.Windows.Forms.Label();
            lOCATION_BREACHLabel = new System.Windows.Forms.Label();
            tOPIC_BREACHLabel = new System.Windows.Forms.Label();
            dESCRIPTION_BREACHLabel = new System.Windows.Forms.Label();
            dATE_BREACHLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // iD_FLOORLabel
            // 
            iD_FLOORLabel.AutoSize = true;
            iD_FLOORLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            iD_FLOORLabel.Location = new System.Drawing.Point(169, 54);
            iD_FLOORLabel.Name = "iD_FLOORLabel";
            iD_FLOORLabel.Size = new System.Drawing.Size(99, 19);
            iD_FLOORLabel.TabIndex = 3;
            iD_FLOORLabel.Text = "Номер этажа:";
            iD_FLOORLabel.Click += new System.EventHandler(this.iD_FLOORLabel_Click);
            // 
            // lOCATION_BREACHLabel
            // 
            lOCATION_BREACHLabel.AutoSize = true;
            lOCATION_BREACHLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lOCATION_BREACHLabel.Location = new System.Drawing.Point(169, 107);
            lOCATION_BREACHLabel.Name = "lOCATION_BREACHLabel";
            lOCATION_BREACHLabel.Size = new System.Drawing.Size(131, 19);
            lOCATION_BREACHLabel.TabIndex = 5;
            lOCATION_BREACHLabel.Text = "Местоположение:";
            // 
            // tOPIC_BREACHLabel
            // 
            tOPIC_BREACHLabel.AutoSize = true;
            tOPIC_BREACHLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            tOPIC_BREACHLabel.Location = new System.Drawing.Point(169, 160);
            tOPIC_BREACHLabel.Name = "tOPIC_BREACHLabel";
            tOPIC_BREACHLabel.Size = new System.Drawing.Size(122, 19);
            tOPIC_BREACHLabel.TabIndex = 7;
            tOPIC_BREACHLabel.Text = "Тема нарушения:";
            // 
            // dESCRIPTION_BREACHLabel
            // 
            dESCRIPTION_BREACHLabel.AutoSize = true;
            dESCRIPTION_BREACHLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dESCRIPTION_BREACHLabel.Location = new System.Drawing.Point(169, 211);
            dESCRIPTION_BREACHLabel.Name = "dESCRIPTION_BREACHLabel";
            dESCRIPTION_BREACHLabel.Size = new System.Drawing.Size(159, 19);
            dESCRIPTION_BREACHLabel.TabIndex = 9;
            dESCRIPTION_BREACHLabel.Text = "Описание нарушения:";
            // 
            // dATE_BREACHLabel
            // 
            dATE_BREACHLabel.AutoSize = true;
            dATE_BREACHLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dATE_BREACHLabel.Location = new System.Drawing.Point(169, 361);
            dATE_BREACHLabel.Name = "dATE_BREACHLabel";
            dATE_BREACHLabel.Size = new System.Drawing.Size(230, 19);
            dATE_BREACHLabel.TabIndex = 11;
            dATE_BREACHLabel.Text = "Дата возникновения нарушения:";
            // 
            // lOCATION_BREACHTextBox
            // 
            this.lOCATION_BREACHTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lOCATION_BREACHTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOCATION_BREACHTextBox.Location = new System.Drawing.Point(173, 131);
            this.lOCATION_BREACHTextBox.Name = "lOCATION_BREACHTextBox";
            this.lOCATION_BREACHTextBox.Size = new System.Drawing.Size(251, 26);
            this.lOCATION_BREACHTextBox.TabIndex = 2;
            this.lOCATION_BREACHTextBox.TextChanged += new System.EventHandler(this.lOCATION_BREACHTextBox_TextChanged);
            // 
            // tOPIC_BREACHTextBox
            // 
            this.tOPIC_BREACHTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tOPIC_BREACHTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tOPIC_BREACHTextBox.Location = new System.Drawing.Point(173, 182);
            this.tOPIC_BREACHTextBox.Name = "tOPIC_BREACHTextBox";
            this.tOPIC_BREACHTextBox.Size = new System.Drawing.Size(251, 26);
            this.tOPIC_BREACHTextBox.TabIndex = 3;
            // 
            // dATE_BREACHDateTimePicker
            // 
            this.dATE_BREACHDateTimePicker.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dATE_BREACHDateTimePicker.Location = new System.Drawing.Point(173, 383);
            this.dATE_BREACHDateTimePicker.Name = "dATE_BREACHDateTimePicker";
            this.dATE_BREACHDateTimePicker.Size = new System.Drawing.Size(251, 26);
            this.dATE_BREACHDateTimePicker.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(78, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(318, 437);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(191, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "Закрыть";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(173, 233);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(251, 125);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(120, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Добавление информации о нарушениях";
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.floorsBindingSource;
            this.comboBox1.DisplayMember = "ID_FLOOR";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(173, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(251, 28);
            this.comboBox1.TabIndex = 13;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 492);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(iD_FLOORLabel);
            this.Controls.Add(lOCATION_BREACHLabel);
            this.Controls.Add(this.lOCATION_BREACHTextBox);
            this.Controls.Add(tOPIC_BREACHLabel);
            this.Controls.Add(this.tOPIC_BREACHTextBox);
            this.Controls.Add(dESCRIPTION_BREACHLabel);
            this.Controls.Add(dATE_BREACHLabel);
            this.Controls.Add(this.dATE_BREACHDateTimePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление информации о нарушениях";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.floorsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource breachesBindingSource;
        private System.Windows.Forms.TextBox lOCATION_BREACHTextBox;
        private System.Windows.Forms.TextBox tOPIC_BREACHTextBox;
        private System.Windows.Forms.DateTimePicker dATE_BREACHDateTimePicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource floorsBindingSource;
    }
}