using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OrionCore.EventManagement;

namespace OrionCore.LogManagement
{
    /// <summary>
    /// Class used to compile error informations used to save a log.
    /// </summary>
    public class OrionLogInfos
    {
        #region Properties
        public EventTypes EventType { get; private set; }
        public String DisplayMessage { get; private set; }
        public String LogMessage { get; private set; }
        public String SourceApplicationName { get; private set; }
        public String Comment1 { get; private set; }
        public String Comment2 { get; private set; }
        public DateTime LogDate { get; private set; }
        public Exception SourceException { get; private set; }
        #endregion

        #region Constructors
        internal OrionLogInfos(String logMessage, String displayMessage, Exception sourceException, String sourceApplicationName, String comment1, String comment2, EventTypes eventType)
        {
            this.LogMessage = logMessage;
            this.DisplayMessage = displayMessage;
            this.SourceException = sourceException;
            this.SourceApplicationName = sourceApplicationName;
            this.Comment1 = comment1;
            this.Comment2 = comment2;
            this.LogDate = DateTime.Now;
            this.EventType = eventType;
        }// OrionLogInfos()
        #endregion

        #region Public interface
        static public Collection<String> ParseStackTrace()
        {
            Boolean bExcludedFile;
            Int32 iMaxLength;
            String strFileNameTemp, strUppercaseFileNameTemp;
            String[] strLineElementsTemp;
            String[] strExcludedSourceFiles = new String[] { "XERRORMANAGER.CS", "XHISTORYFILE.CS" };
            Collection<String> strLines;
            List<String[]> strLinesElements;
            StackFrame xStackFrameTemp;
            StackFrame[] xStackFrames;
            StackTrace xStack;

            iMaxLength = 0;
            strLines = new Collection<String>();
            strLinesElements = new List<String[]>();

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
        #endregion
    }
}