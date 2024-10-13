namespace FolderSync
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonAddFolders = new Button();
            listBoxFolders = new ListBox();
            groupBoxSourceFolders = new GroupBox();
            buttonServeice = new Button();
            maskedTextBoxTime = new MaskedTextBox();
            label1 = new Label();
            groupBoxSourceFolders.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAddFolders
            // 
            buttonAddFolders.Location = new Point(259, 24);
            buttonAddFolders.Name = "buttonAddFolders";
            buttonAddFolders.Size = new Size(92, 191);
            buttonAddFolders.TabIndex = 0;
            buttonAddFolders.Text = "Добавить папку";
            buttonAddFolders.UseVisualStyleBackColor = true;
            buttonAddFolders.Click += buttonAddFolders_Click;
            // 
            // listBoxFolders
            // 
            listBoxFolders.FormattingEnabled = true;
            listBoxFolders.ItemHeight = 17;
            listBoxFolders.Location = new Point(6, 24);
            listBoxFolders.Name = "listBoxFolders";
            listBoxFolders.Size = new Size(247, 191);
            listBoxFolders.TabIndex = 1;
            listBoxFolders.MouseDoubleClick += listBox1_MouseDoubleClick;
            // 
            // groupBoxSourceFolders
            // 
            groupBoxSourceFolders.Controls.Add(listBoxFolders);
            groupBoxSourceFolders.Controls.Add(buttonAddFolders);
            groupBoxSourceFolders.Location = new Point(12, 12);
            groupBoxSourceFolders.Name = "groupBoxSourceFolders";
            groupBoxSourceFolders.Size = new Size(357, 236);
            groupBoxSourceFolders.TabIndex = 2;
            groupBoxSourceFolders.TabStop = false;
            groupBoxSourceFolders.Text = "Список папок для синхронизации";
            // 
            // buttonServeice
            // 
            buttonServeice.Location = new Point(12, 254);
            buttonServeice.Name = "buttonServeice";
            buttonServeice.Size = new Size(357, 24);
            buttonServeice.TabIndex = 3;
            buttonServeice.Text = "Service";
            buttonServeice.UseVisualStyleBackColor = true;
            buttonServeice.Click += buttonServeice_Click;
            // 
            // maskedTextBoxTime
            // 
            maskedTextBoxTime.Location = new Point(264, 284);
            maskedTextBoxTime.Mask = "00:00";
            maskedTextBoxTime.Name = "maskedTextBoxTime";
            maskedTextBoxTime.Size = new Size(105, 25);
            maskedTextBoxTime.TabIndex = 4;
            maskedTextBoxTime.Text = "1200";
            maskedTextBoxTime.ValidatingType = typeof(DateTime);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(163, 287);
            label1.Name = "label1";
            label1.Size = new Size(95, 17);
            label1.TabIndex = 5;
            label1.Text = "Время запуска";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 387);
            Controls.Add(label1);
            Controls.Add(maskedTextBoxTime);
            Controls.Add(buttonServeice);
            Controls.Add(groupBoxSourceFolders);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Синхронизатор папок";
            Load += Form1_Load;
            groupBoxSourceFolders.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonAddFolders;
        private ListBox listBoxFolders;
        private GroupBox groupBoxSourceFolders;
        private Button buttonServeice;
        private MaskedTextBox maskedTextBoxTime;
        private Label label1;
    }
}