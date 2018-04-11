using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using OrionCore.LogManagement;

namespace OrionCore.EventManagement
{
    public class OrionEventManager
    {
        #region Properties
        /// <summary>
        /// Get an <see cref="OrionLogInfos"/> class containing last event informations.
        /// </summary>
        /// <remarks>This class can be used with IOrionLogManager object to record event log informations</remarks>
        public OrionLogInfos Log { get; private set; }
        /// <summary>
        /// Get or set an <see cref="IOrionLogManager"/> object to save event log informations in a storage location (file, database and so on...).
        /// </summary>
        public IOrionLogManager LogManager1 { get; set; }
        /// <summary>
        /// Get or set another <see cref="IOrionLogManager"/> object to save event log informations in a storage location (file, database and so on...) if the first one failed to do it.
        /// </summary>
        public IOrionLogManager LogManager2 { get; set; }
        #endregion

        #region Constructors
        public OrionEventManager()
            : this(null, null)
        {
        }// OrionErrorManager()
        public OrionEventManager(IOrionLogManager logManager)
            : this(logManager, null)
        {
        }// OrionEventManager()
        public OrionEventManager(IOrionLogManager logManager1, IOrionLogManager logManager2)
        {
            if (logManager2 != null && LogManager1 == null)
                this.LogManager1 = logManager2;
            else
            {
                this.LogManager1 = logManager1;
                this.LogManager2 = logManager2;
            }
        }// OrionEventManager()
        #endregion

        #region Public interface
        /// <summary>
        /// Reports an event with the specified event log message.
        /// </summary>
        /// <remarks>No exception information will be reported. Only a log message can be retreived.</remarks>
        public OrionLogInfos ReportEvent(String logMessage, String comment1 = null, String comment2 = null)
        {
            return this.ReportEvent(logMessage, null, null, EventTypes.Information, comment1, comment2);
        }// ReportEvent()
        /// <summary>
        /// Reports an event with the specified event log message.
        /// </summary>
        /// <remarks>No exception information will be reported. Only a log message can be retreived.</remarks>
        public OrionLogInfos ReportEvent(String logMessage, EventTypes eventType, String comment1 = null, String comment2 = null)
        {
            return this.ReportEvent(logMessage, null, null, eventType, comment1, comment2);
        }// ReportEvent()
        /// <summary>
        /// Reports an event with the specified event log message and display event message. Event type is set to <see cref="EventTypes.Information" /> type.
        /// </summary>
        /// <remarks>No exception information will be reported. Only event messages can be retreived.</remarks>
        public OrionLogInfos ReportEvent(String logMessage, String displayEventMessage, String comment1 = null, String comment2 = null)
        {
            return this.ReportEvent(logMessage, displayEventMessage, null, EventTypes.Information, comment1, comment2);
        }// ReportEvent()
        /// <summary>
        /// Reports an event with the specified event log message and display event message.
        /// </summary>
        /// <remarks>No exception information will be reported. Only event messages can be retreived.</remarks>
        public OrionLogInfos ReportEvent(String logMessage, String displayEventMessage, EventTypes eventType, String comment1 = null, String comment2 = null)
        {
            return this.ReportEvent(logMessage, displayEventMessage, null, eventType, comment1, comment2);
        }// ReportEvent()
        /// <summary>
        /// Reports an event with the specified event log message, display event message and source exception. Event type is set to <see cref="EventTypes.Error" /> type.
        /// </summary>
        public OrionLogInfos ReportEvent(String logMessage, String displayMessage, Exception ex, String comment1 = null, String comment2 = null)
        {
            return this.ReportEvent(logMessage, displayMessage, ex, EventTypes.Error, comment1, comment2);
        }// ReportError()
        /// <summary>
        /// Reports an event with the specified event log message, display event message and source exception.
        /// </summary>
        public OrionLogInfos ReportEvent(String logMessage, String displayMessage, Exception ex, EventTypes eventType, String comment1 = null, String comment2 = null)
        {
            Boolean bLogSuccessfullyReported;
            Assembly xAssembly;

            bLogSuccessfullyReported = false;

            xAssembly = Assembly.GetEntryAssembly();
            if (xAssembly == null) xAssembly = Assembly.GetCallingAssembly();
            this.Log = new OrionLogInfos(logMessage, displayMessage, ex, xAssembly.GetName().Name, comment1, comment2, eventType);

            //** Try using first logManager to record log, and the second first one failed. **
            if (this.LogManager1 != null) bLogSuccessfullyReported = this.LogManager1.SaveLog(this.Log);
            if (bLogSuccessfullyReported == false && this.LogManager2 != null) bLogSuccessfullyReported = this.LogManager2.SaveLog(this.Log);

            Messenger.Default.Send<OrionMessageEventReporting>(new OrionMessageEventReporting(this.Log.EventType));

            return this.Log;
        }// ReportEvent()
        public void Reset()
        {
            this.Log = null;
        }// Reset()
        #endregion
    }
}
