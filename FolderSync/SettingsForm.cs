using FolderSyncLib.Models;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderSync
{
    public partial class SettingsForm : Form
    {
        public SettingsModel SettingsModel { get; set; }
        SyncFolderModel SyncFolderModel { get; set; }
        MainForm mainForm;
        public SettingsForm(SyncFolderModel syncFolderModel, MainForm mainForm)
        {
            InitializeComponent();
            this.SyncFolderModel = syncFolderModel;
            SettingsModel = SyncFolderModel.Settings;
            this.mainForm = mainForm;
            checkBoxIsActive.Checked = !SyncFolderModel.Settings.IsSkip;
            if (SyncFolderModel.Settings.LastUpdate == DateTime.MinValue) textBoxInfo.Text = $"Не синхронезирован";
            else textBoxInfo.Text = $"Синхронизировани:[{SyncFolderModel.Settings.LastUpdate.ToString("f")}]";
        }
        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            checkBoxIsActive.Checked = true;
            textBoxInfo.Text = $"Не синхронезирован";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show($"Удалить связку {SyncFolderModel}?", "Удалить?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                mainForm.DeleteFolderFromList(SyncFolderModel);

            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingsModel == null) SettingsModel = new SettingsModel
            {
                IsSkip = !checkBoxIsActive.Checked,
            };
            if(SyncFolderModel != null) SyncFolderModel.Settings = SettingsModel;
            if (SettingsModel != null) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;
        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

