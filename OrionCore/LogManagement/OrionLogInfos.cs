using System;

namespace OrionCore.LogManagement
{
    /// <summary>
    /// Class used to compile error informations used to save a log.
    /// </summary>
    public class OrionLogInfos
    {
        #region Properties
        public LogTypes LogType { get; private set; }
        public String DisplayMessage { get; private set; }
        public String LogMessage { get; private set; }
        public String SourceApplicationName { get; private set; }
        public DateTime LogDate { get; private set; }
        public Exception SourceException { get; private set; }
        #endregion

        #region Constructors
        internal OrionLogInfos(String logMessage, String displayMessage, Exception sourceException, String sourceApplicationName, LogTypes logType)
        {
            this.LogMessage = logMessage;
            this.DisplayMessage = displayMessage;
            this.SourceException = sourceException;
            this.SourceApplicationName = sourceApplicationName;
            this.LogDate = DateTime.Now;
            this.LogType = logType;
        }// OrionLogInfos()
        #endregion
    }
}
