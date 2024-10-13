using FolderSyncLib.Models;
using Microsoft.Win32.TaskScheduler;
using Syroot.Windows.IO;

namespace FolderSync;

public partial class MainForm : Form
{
    List<SyncFolderModel> FoldersList { get; set; }
    public MainForm()
    {
        InitializeComponent();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        ReloadFolders();
        Checkfssync();
        CheckSettings();
    }

    private void CheckSettings()
    {
        if (IsServiceAdded())
        {
            buttonServeice.BackColor = Color.LightGreen;
            buttonServeice.Text = "Сервис запущен. Остановить";
            var time = GetServiceRunTime();
            if(time != TimeSpan.Zero)
            {
                maskedTextBoxTime.Text = time.ToString(@"hh\:mm");
            }
        }
        else
        {
            buttonServeice.BackColor = Color.LightPink;
            buttonServeice.Text = "Сервис НЕ запущен. Запустить";
        }
    }

    private void Checkfssync()
    {
        var fsconsole = Path.Combine(KnownFolders.LocalAppData.Path, "fsconsole.exe");
        if (!File.Exists(fsconsole))
        {
            File.WriteAllBytes(fsconsole, Properties.Resources.fsconsole);
        }
        else if (new FileInfo(fsconsole).Length != Properties.Resources.fsconsole.Length)
        {
            File.WriteAllBytes(fsconsole, Properties.Resources.fsconsole);
        }
    }

    private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        var selectedFolderDiscriprion = listBoxFolders?.SelectedItem?.ToString();
        if (selectedFolderDiscriprion == null) return;
        var selectedFolder = FoldersList.FirstOrDefault(x => x.ToString() == selectedFolderDiscriprion);
        if (selectedFolder == null)
        {
            ReloadFolders();
            return;
        }
        var sf = new SettingsForm(selectedFolder, this);
        sf.ShowDialog();
        SyncFolderModel.SaveList(FoldersList);
        ReloadFolders();
    }
    private void buttonAddFolders_Click(object sender, EventArgs e)
    {
        using (var fbdSource = new FolderBrowserDialog())
        {
            fbdSource.Description = "Папка источник ->";
            fbdSource.ShowNewFolderButton = false;
            if (fbdSource.ShowDialog() == DialogResult.OK)
            {
                var fSource = fbdSource.SelectedPath;
                using (var fbdDist = new FolderBrowserDialog())
                {
                    fbdDist.Description = "-> Папка приемник";
                    fbdDist.ShowNewFolderButton = true;
                    if (fbdDist.ShowDialog() == DialogResult.OK)
                    {
                        var fDist = fbdDist.SelectedPath;
                        var sf = new SettingsForm(this);
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            FoldersList.Add(new SyncFolderModel(fSource, fDist, sf.SettingsModel));
                            SyncFolderModel.SaveList(FoldersList);
                            ReloadFolders();
                        }
                        else return;
                    }
                    else return;
                }
            }
            else return;
        }
    }
    private void ReloadFolders()
    {
        listBoxFolders.Items.Clear();
        FoldersList = SyncFolderModel.LoadList();
        if (FoldersList == null)
        {
            FoldersList = new List<SyncFolderModel>();
            return;
        }
        foreach (var folder in FoldersList)
        {
            listBoxFolders?.Items.Add(folder.ToString());
        }
    }
    public void DeleteFolderFromList(SyncFolderModel syncFolderModel)
    {
        FoldersList.Remove(syncFolderModel);
        SyncFolderModel.SaveList(FoldersList);
        ReloadFolders();
    }

    private void buttonServeice_Click(object sender, EventArgs e)
    {
        if (!IsServiceAdded())
        {
            AddService();
            buttonServeice.BackColor = Color.LightGreen;
            buttonServeice.Text = "Сервис запущен. Остановить";
        }
        else
        {
            DeleteServeice();
            buttonServeice.BackColor = Color.LightPink;
            buttonServeice.Text = "Сервис НЕ запущен. Запустить";
        }

    }
    private void AddService()
    {
        int h , m = 0;
        try
        {
            h = int.Parse(maskedTextBoxTime.Text.Split(':')[0]);
            m = int.Parse(maskedTextBoxTime.Text.Split(':')[1]);
        }
        catch
        {
            h = 12;
            m = 0;
        }
        using (TaskService ts = new TaskService())
        {// Создаем новую задачу в планеровщике
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Синхронизация папок";
            td.Triggers.Add(new DailyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(h) + TimeSpan.FromMinutes(m) });
            td.Actions.Add(new ExecAction("fsconsole.exe", null, KnownFolders.LocalAppData.Path));
            ts.RootFolder.RegisterTaskDefinition(@"FolderSync", td);
        }
    }

    private bool IsServiceAdded()
    {
        using (TaskService ts = new TaskService())
        {
            var result = ts.FindTask("FolderSync");
            return result != null;
        }
    }
    private void DeleteServeice()
    {
        using (TaskService ts = new TaskService())
        {
            ts.RootFolder.DeleteTask("FolderSync", false);
        }
    }
    private TimeSpan GetServiceRunTime()
    {
        using (TaskService ts = new TaskService())
        {
            var task = ts.FindTask("FolderSync");
            if (task != null)
            {
                DateTime? nextRunTime = task.NextRunTime;
                if (nextRunTime.HasValue) return TimeSpan.FromHours(nextRunTime.Value.Hour) + TimeSpan.FromMinutes(nextRunTime.Value.Minute);
                else return TimeSpan.Zero;
            }
            else return TimeSpan.Zero;
        }
    }
}