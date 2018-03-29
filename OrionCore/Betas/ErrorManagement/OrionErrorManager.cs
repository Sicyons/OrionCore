using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace OrionCore.ErrorManagement
{
    public class OrionErrorManager
    {
        #region Properties
        /// <summary>
        /// Get an <see cref="StructOrionErrorLogInfos"/> structure containing last error informations.
        /// </summary>
        /// <remarks>This structure can be used with IOrionErrorLogManager object to record log informations</remarks>
        public StructOrionErrorLogInfos ErrorLog { get; private set; }
        /// <summary>
        /// Get or set an <see cref="IOrionErrorLogManager"/> object to save error informations in a storage location (file, database and so on...).
        /// </summary>
        public IOrionErrorLogManager LogManager1 { get; set; }
        /// <summary>
        /// Get or set another <see cref="IOrionErrorLogManager"/> object to save error informations in a storage location (file, database and so on...) if the first one failed to do it.
        /// </summary>
        public IOrionErrorLogManager LogManager2 { get; set; }
        #endregion

        #region Constructors
        public OrionErrorManager()
            : this(null, null)
        {
        }// OrionErrorManager()
        public OrionErrorManager(IOrionErrorLogManager logManager)
            : this(logManager, null)
        {
        }// OrionErrorManager()
        public OrionErrorManager(IOrionErrorLogManager logManager1, IOrionErrorLogManager logManager2)
        {
            if (logManager2 != null && LogManager1 == null)
                this.LogManager1 = logManager2;
            else
            {
                this.LogManager1 = logManager1;
                this.LogManager2 = logManager2;
            }
        }// OrionErrorManager()
        #endregion

        #region Public interface
        static public Collection<String> ParseStackTrace()
        {
            Boolean bExcludedFile;
            Int32 iMaxLength;
            String strFileNameTemp, strUppercaseFileNameTemp;
            String[] strLineElementsTemp;
            String[] strExcludedSourceFiles = new String[] { "ORIONERRORMANAGER.CS", "ORIONHISTORYFILE.CS" };
            Collection<String> strLines;
            Collection<String[]> strLinesElements;
            StackFrame xStackFrameTemp;
            StackFrame[] xStackFrames;
            StackTrace xStack;

            iMaxLength = 0;
            strLines = new Collection<String>();
            strLinesElements = new Collection<String[]>();

            xStack = new StackTrace(true);
            xStackFrames = xStack.GetFrames();

            for (Int32 iLineCounter = 0; iLineCounter < xStackFrames.Length; iLineCounter++)
            {
                bExcludedFile = false;
                xStackFrameTemp = xStackFrames[iLineCounter];

                strFileNameTemp = Path.GetFileName(xStackFrameTemp.GetFileName());
                if (String.IsNullOrWhiteSpace(strFileNameTemp) == false)
                {
                    //** Check excluded source files. **
                    foreach (String strExcludedSourceFiletemp in strExcludedSourceFiles)
                    {
                        strUppercaseFileNameTemp = strFileNameTemp.ToUpperInvariant();
                        if (strUppercaseFileNameTemp == strExcludedSourceFiletemp)
                        {
                            bExcludedFile = true;
                            break;
                        }
                    }

                    if (bExcludedFile == false)
                    {
                        iMaxLength = Math.Max(iMaxLength, strFileNameTemp.Length);
                        strLineElementsTemp = new String[2];

                        strLineElementsTemp[0] = strFileNameTemp;
                        strLineElementsTemp[1] = xStackFrameTemp.GetMethod().Name + "()";

                        strLinesElements.Add(new String[] { strLineElementsTemp[0], strLineElementsTemp[1] });
                    }
                }
            }

            foreach (String[] strLineElementsTemp2 in strLinesElements)
                strLines.Add(strLineElementsTemp2[0] = strLineElementsTemp2[0].PadLeft(iMaxLength) + " -> " + strLineElementsTemp2[1]);

            return strLines;
        }// ParseStackTrace()
        /// <summary>
        /// Reports an error with the specified error log message.
        /// </summary>
        /// <remarks>No exception information will be reported. Only an error message can be retreived.</remarks>
        public void ReportError(String logMessage)
        {
            this.ReportError(logMessage, null);
        }// ReportError()
        /// <summary>
        /// Reports an error with the specified error log message and display error message.
        /// </summary>
        /// <remarks>No exception information will be reported. Only error messages can be retreived.</remarks>
        public void ReportError(String logMessage, String displayErrorMessage)
        {
            this.ReportError(logMessage, displayErrorMessage, null);
        }// ReportError()
        /// <summary>
        /// Reports an error with the specified error log message, display error message and source exception.
        /// </summary>
        public void ReportError(String logMessage, String displayMessage, Exception ex)
        {
            Boolean bLogSuccessfullyReported;
            Assembly xAssembly;

            bLogSuccessfullyReported = false;

            xAssembly = Assembly.GetEntryAssembly();
            if (xAssembly == null) xAssembly = Assembly.GetCallingAssembly();
            this.ErrorLog = new StructOrionErrorLogInfos(logMessage, displayMessage, ex, xAssembly.GetName().Name);

            //** Try using first logManager to record log, and the second first one failed. **
            if (this.LogManager1 != null) bLogSuccessfullyReported = this.LogManager1.LogError(this.ErrorLog);
            if (bLogSuccessfullyReported == false && this.LogManager2 != null) bLogSuccessfullyReported = this.LogManager2.LogError(this.ErrorLog);

            //this.ErrorReported = true;
        }// ReportError()
        #endregion
    }
}
