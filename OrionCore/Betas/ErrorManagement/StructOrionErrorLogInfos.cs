using System;

namespace OrionCore.ErrorManagement
{
    /// <summary>
    /// Struct used to compile error informations used to save an error log.
    /// </summary>
    public struct StructOrionErrorLogInfos
    {
        #region Properties
        public String DisplayMessage { get; private set; }
        public String LogMessage { get; private set; }
        public String SourceApplicationName { get; private set; }
        public DateTime LogDate { get; private set; }
        public Exception SourceException { get; private set; }
        #endregion

        #region Constructors
        internal StructOrionErrorLogInfos(String logMessage, String displayMessage, Exception sourceException, String sourceApplicationName)
        {
            this.LogMessage = logMessage;
            this.DisplayMessage = displayMessage;
            this.SourceException = sourceException;
            this.SourceApplicationName = sourceApplicationName;
            this.LogDate = DateTime.Now;
        }// StructOrionErrorLogInfos()
        #endregion
    }
}
