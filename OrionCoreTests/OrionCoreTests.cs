using System;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrionCore.ErrorManagement;
using OrionFiles;
using OrionCore;

namespace OrionCoreTests
{
    [TestClass]
    public class OrionCoreTests
    {
        #region Fields
        private static Exception xInnerException = new ArgumentException("Test exception;");
        private static String strLogFileDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestLogs");
        private static String strLogFilePath = Path.Combine(strLogFileDirectory, "Error.log");
        private static OrionHistoryFile xOrionHistoryFile;
        #endregion

        #region Initializations
        [TestInitialize]
        public void Initialize()
        {
            if (String.IsNullOrWhiteSpace(OrionCoreTests.strLogFileDirectory) == false)
            {
                if (Directory.Exists(OrionCoreTests.strLogFileDirectory) == false) Directory.CreateDirectory(OrionCoreTests.strLogFileDirectory);
                if (File.Exists(OrionCoreTests.strLogFilePath) == true) File.Delete(OrionCoreTests.strLogFilePath);

                OrionCoreTests.xOrionHistoryFile = new OrionHistoryFile(strLogFilePath);
            }
        }// Initialize()
        #endregion

        #region Test methods
        #region OrionDeployementInfos
        [TestCategory("OrionDeploymentInfos")]
        [TestMethod]
        public void OrionDeployementInfos_DataFolder_IsBaseDirectory()
        {
            Assert.AreEqual(OrionDeploymentInfos.DataFolder, AppDomain.CurrentDomain.BaseDirectory);
        }// OrionDeploymentInfos_DataFolder_IsBaseDirectory()
        [TestCategory("OrionDeploymentInfos")]
        [TestMethod]
        public void OrionDeployementInfos_UpdateFolder_XDeployment_DataFolder_IsBaseDirectoryUpdates()
        {
            Assert.AreEqual(OrionDeploymentInfos.UpdateFolder, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates"));
        }// OrionDeploymentInfos_UpdateFolder_XDeployment_DataFolder_IsBaseDirectoryUpdates()
        [TestCategory("OrionDeploymentInfos")]
        [TestMethod]
        public void OrionDeployementInfos_Version_NotNull()
        {
            Assert.IsNotNull(OrionDeploymentInfos.ApplicationVersion);
        }// OrionDeploymentInfos_Version_NotNull()
        [TestCategory("OrionDeploymentInfos")]
        [TestMethod]
        public void OrionDeployementInfos_VersionPublication_Dash()
        {
            Assert.AreEqual(OrionDeploymentInfos.PublicationVersion, String.Empty);
        }// OrionDeploymentInfos_VersionPublication_Dash()
        #endregion

        #region XException
        [TestCategory("OrionException")]
        [TestMethod]
        public void CreateOrionException_EmptyException_IsCreated()
        {
            OrionException xOrionException;

            xOrionException = new OrionException();

            Assert.IsNotNull(xOrionException);
        }// CreateOrionException_EmptyException_IsCreated()
        [TestCategory("OrionException")]
        [TestMethod]
        public void CreateOrionException_CustomMessage_IsCreated()
        {
            OrionException xOrionException;

            xOrionException = new OrionException("Test message;");

            Assert.IsNotNull(xOrionException);
            Assert.AreEqual(xOrionException.Message, "Test message;");
        }// CreateOrionException_CustomMessage_IsCreated()
        [TestCategory("OrionException")]
        [TestMethod]
        public void CreateOrionException_CustomMessageData_IsCreated()
        {
            OrionException xOrionException;

            xOrionException = new OrionException("Test message;", "DataTest1=DataValue1", "DataTest2=DataValue2", "DataTest3=DataValue3");

            Assert.IsNotNull(xOrionException);
            Assert.AreEqual(xOrionException.Message, "Test message;");
            Assert.IsNotNull(xOrionException.Data);
            Assert.AreEqual(xOrionException.Data.Contains("DataTest1"), true);
            Assert.AreEqual(xOrionException.Data["DataTest1"], "DataValue1");
            Assert.AreEqual(xOrionException.Data.Contains("DataTest2"), true);
            Assert.AreEqual(xOrionException.Data["DataTest2"], "DataValue2");
            Assert.AreEqual(xOrionException.Data.Contains("DataTest3"), true);
            Assert.AreEqual(xOrionException.Data["DataTest3"], "DataValue3");
        }// CreateOrionException_CustomMessageData_IsCreated()
        [TestCategory("OrionException")]
        [TestMethod]
        public void CreateOrionException_CustomMessageInnerException_IsCreated()
        {
            OrionException xOrionException;

            xOrionException = new OrionException("Test message;", OrionCoreTests.xInnerException);

            Assert.IsNotNull(xOrionException);
            Assert.AreEqual(xOrionException.Message, "Test message;");
            Assert.IsNotNull(xOrionException.InnerException);
            Assert.IsInstanceOfType(xOrionException.InnerException, typeof(ArgumentException));
        }// CreateOrionException_CustomMessageInnerException_IsCreated()
        [TestCategory("OrionException")]
        [TestMethod]
        public void CreateOrionException_CustomMessageInnerExceptionData_IsCreated()
        {
            OrionException xOrionException;

            xOrionException = new OrionException("Test message;", OrionCoreTests.xInnerException, "DataTest1=DataValue1", "DataTest2=DataValue2", "DataTest3=DataValue3");

            Assert.IsNotNull(xOrionException);
            Assert.AreEqual(xOrionException.Message, "Test message;");
            Assert.IsNotNull(xOrionException.InnerException);
            Assert.IsInstanceOfType(xOrionException.InnerException, typeof(ArgumentException));
            Assert.IsNotNull(xOrionException.Data);
            Assert.AreEqual(xOrionException.Data.Contains("DataTest1"), true);
            Assert.AreEqual(xOrionException.Data["DataTest1"], "DataValue1");
            Assert.AreEqual(xOrionException.Data.Contains("DataTest2"), true);
            Assert.AreEqual(xOrionException.Data["DataTest2"], "DataValue2");
            Assert.AreEqual(xOrionException.Data.Contains("DataTest3"), true);
            Assert.AreEqual(xOrionException.Data["DataTest3"], "DataValue3");
        }// CreateOrionException_CustomMessageInnerExceptionData_IsCreated()
        #endregion

        #region OrionErrorManager
        #region XErrorManager Creation
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void CreateOrionErrorManager_Empty_IsCreated()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager();

            Assert.IsNotNull(xOrionErrorManager);
        }// CreateOrionErrorManager_Empty_IsCreated()
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void CreateOrionErrorManager_OrionHistoryFile1_IsCreated()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(OrionCoreTests.xOrionHistoryFile);

            Assert.IsNotNull(xOrionErrorManager);
            Assert.IsInstanceOfType(xOrionErrorManager.LogManager1, typeof(OrionHistoryFile));
        }// CreateOrionErrorManager_OrionHistoryFile1_IsCreated()
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void CreateOrionErrorManager_OrionHistoryFile2_IsCreated()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(null, OrionCoreTests.xOrionHistoryFile);

            Assert.IsNotNull(xOrionErrorManager);
            Assert.IsInstanceOfType(xOrionErrorManager.LogManager1, typeof(OrionHistoryFile));
        }// CreateOrionErrorManager_OrionHistoryFile2_IsCreated()
        #endregion

        #region Error reporting
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void ErrorReporting_Message_Reported()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(OrionCoreTests.xOrionHistoryFile);
            xOrionErrorManager.ReportError("Test Message;");

            Assert.IsNotNull(xOrionErrorManager.ErrorLog);
            Assert.AreEqual(xOrionErrorManager.ErrorLog.LogMessage, "Test Message;");
        }// ErrorReporting_Message_Reported()
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void ErrorReporting_MessageException_Reported()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(OrionCoreTests.xOrionHistoryFile);
            xOrionErrorManager.ReportError("Test Message;", null, new OrionException("Test exception;"));

            Assert.IsNotNull(xOrionErrorManager.ErrorLog);
            Assert.AreEqual(xOrionErrorManager.ErrorLog.LogMessage, "Test Message;");
            Assert.IsInstanceOfType(xOrionErrorManager.ErrorLog.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionErrorManager.ErrorLog.SourceException.Message, "Test exception;");
        }// ErrorReporting_MessageException_Reported()
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void ErrorReporting_MessageDisplayMessageException_Reported()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(OrionCoreTests.xOrionHistoryFile);
            xOrionErrorManager.ReportError("Test Message;", "Test display message;", new OrionException("Test exception;"));

            Assert.IsNotNull(xOrionErrorManager.ErrorLog);
            Assert.AreEqual(xOrionErrorManager.ErrorLog.LogMessage, "Test Message;");
            Assert.AreEqual(xOrionErrorManager.ErrorLog.DisplayMessage, "Test display message;");
            Assert.IsInstanceOfType(xOrionErrorManager.ErrorLog.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionErrorManager.ErrorLog.SourceException.Message, "Test exception;");
        }// ErrorReporting_MessageDisplayMessageException_Reported()
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void ErrorReporting_Reset_Reseted()
        {
            OrionErrorManager xOrionErrorManager;

            xOrionErrorManager = new OrionErrorManager(OrionCoreTests.xOrionHistoryFile);
            xOrionErrorManager.ReportError("Test Message;");
            xOrionErrorManager.Reset();

            Assert.IsNull(xOrionErrorManager.ErrorLog);
        }//ErrorReporting_Reset_Reseted()
        #endregion

        #region Miscellaneous
        [TestCategory("OrionErrorManager")]
        [TestMethod]
        public void ParseStackTrace_Parse_Parsed()
        {
            Boolean bWellFormattedLines;
            Collection<String> strResultats;

            bWellFormattedLines = true;

            strResultats = OrionErrorManager.ParseStackTrace();
            foreach (String strResultatTemp in strResultats)
                if (strResultatTemp.Split(new String[] { " -> " }, StringSplitOptions.None).Length != 2)
                {
                    bWellFormattedLines = false;
                    break;
                }

            Assert.IsTrue(bWellFormattedLines);
        }// ParseStackTrace_Parse_Parsed()
        #endregion
        #endregion
        #endregion
    }
}