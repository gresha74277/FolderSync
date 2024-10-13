namespace FolderSync
{
    partial class SettingsForm
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
            buttonDelete = new Button();
            checkBoxIsActive = new CheckBox();
            buttonClose = new Button();
            textBoxInfo = new TextBox();
            SuspendLayout();
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.FromArgb(255, 192, 192);
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Location = new Point(12, 9);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(120, 24);
            buttonDelete.TabIndex = 2;
            buttonDelete.Text = "Удалить из листа";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // checkBoxIsActive
            // 
            checkBoxIsActive.AutoSize = true;
            checkBoxIsActive.Checked = true;
            checkBoxIsActive.CheckState = CheckState.Checked;
            checkBoxIsActive.Location = new Point(160, 12);
            checkBoxIsActive.Name = "checkBoxIsActive";
            checkBoxIsActive.Size = new Size(74, 21);
            checkBoxIsActive.TabIndex = 3;
            checkBoxIsActive.Text = "Активна";
            checkBoxIsActive.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = Color.FromArgb(192, 255, 192);
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Location = new Point(185, 114);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(49, 24);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "OK";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click_1;
            // 
            // textBoxInfo
            // 
            textBoxInfo.BorderStyle = BorderStyle.None;
            textBoxInfo.Location = new Point(12, 39);
            textBoxInfo.Multiline = true;
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.ReadOnly = true;
            textBoxInfo.Size = new Size(222, 69);
            textBoxInfo.TabIndex = 6;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(246, 149);
            Controls.Add(textBoxInfo);
            Controls.Add(buttonClose);
            Controls.Add(checkBoxIsActive);
            Controls.Add(buttonDelete);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SettingsForm";
            FormClosing += SettingsForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonDelete;
        private CheckBox checkBoxIsActive;
        private Button buttonClose;
        private TextBox textBoxInfo;
    }
}