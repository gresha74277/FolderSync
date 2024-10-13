using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FolderSyncLib.Models
{
    public class SettingsModel
    {
        [JsonProperty("LastUpdate")]
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;
        [JsonProperty("IsSkip")]
        public bool IsSkip { get; set; } = false;
    }
}
