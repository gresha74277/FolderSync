using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syroot.Windows.IO;

namespace FolderSyncLib.Models
{
    public class SyncFolderModel
    {
        [JsonProperty("id")]
        public int Id { get; }
        [JsonProperty("SourceFolder")]
        public string SourceFolder { get; set; }
        [JsonProperty("DestinationFolder")]
        public string DestinationFolder { get; set; }
        [JsonProperty("Discription")]
        public string Discription { get; set; } = string.Empty;
        public SettingsModel Settings { get; set; }
        public SyncFolderModel(string sourceFolder, string destinationFolder, SettingsModel settings)
        {
            Id = LastId++;
            SourceFolder = sourceFolder;
            DestinationFolder = destinationFolder;
            Settings = settings;
        }

        #region Static funcs(Load, Save...)
        [JsonIgnore]
        public static int LastId { get; set; }
        [JsonIgnore]
        public static string DefaultListFileName
        {
            get => Path.ChangeExtension(defaultListFileName, extention);
            set => defaultListFileName = Path.GetFileNameWithoutExtension(value);
        }
        [JsonIgnore]
        private static string defaultListFileName = "folderSyncList";
        [JsonIgnore]
        const string extention = ".fsl";
        public static List<SyncFolderModel>? LoadList()
        {
            var listFilePath = Path.Combine(KnownFolders.LocalAppData.Path, DefaultListFileName);
            if (!File.Exists(listFilePath)) return null;
            try
            {
                var json = File.ReadAllText(listFilePath);
                var list = JsonConvert.DeserializeObject<List<SyncFolderModel>>(json);
                return list;
            }
            catch (Exception ex)
            {
                Loging.AddLog(ex);
                return null;
            }
        }
        public static bool SaveList(List<SyncFolderModel> list)
        {
            if (list == null) return false;
            var listFilePath = Path.Combine(KnownFolders.LocalAppData.Path, DefaultListFileName);
            try
            {
                var json = JsonConvert.SerializeObject(list);
                File.WriteAllText(listFilePath, json);
                return true;
            }
            catch(Exception ex)
            {
                Loging.AddLog(ex);
                return false;
            }
        }
        #endregion
        public override string ToString()
        {
            return $"{this.SourceFolder} -> {this.DestinationFolder}";
        }
    }
}
