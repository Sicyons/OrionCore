using System;

namespace OrionCore.ErrorManagement
{
    /// <summary>
    /// Class used to compile error informations used to save an error log.
    /// </summary>
    public class OrionErrorLogInfos
    {
        #region Properties
        public ErrorTypes ErrorType { get; private set; }
        public String DisplayMessage { get; private set; }
        public String LogMessage { get; private set; }
        public String SourceApplicationName { get; private set; }
        public DateTime LogDate { get; private set; }
        public Exception SourceException { get; private set; }
        #endregion

        #region Constructors
        internal OrionErrorLogInfos(String logMessage, String displayMessage, Exception sourceException, String sourceApplicationName, ErrorTypes errorType)
        {
            this.LogMessage = logMessage;
            this.DisplayMessage = displayMessage;
            this.SourceException = sourceException;
            this.SourceApplicationName = sourceApplicationName;
            this.LogDate = DateTime.Now;
            this.ErrorType = errorType;
        }// OrionErrorLogInfos()
        #endregion
    }
}
