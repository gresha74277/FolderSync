using System;
using System.Collections.Generic;
using System.Text;

namespace FolderSyncLib
{
    public static class Loging
    {
        public delegate void LogDelegate(Exception ex);
        public static event LogDelegate LogEvent;
        public static void AddLog(Exception ex) => LogEvent?.Invoke(ex);
    }
}
