using System;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalaSoft.MvvmLight.Messaging;
using OrionCore.ErrorManagement;
using OrionCore.EventManagement;
using OrionCore.LogManagement;
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

        #region OrionEventManager
        #region OrionEventManager Creation
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void CreateOrionEventManager_Empty_IsCreated()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager();

            Assert.IsNotNull(xOrionEventManager);
        }// CreateOrionEventManager_Empty_IsCreated()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void CreateOrionEventManager_OrionHistoryFile1_IsCreated()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);

            Assert.IsNotNull(xOrionEventManager);
            Assert.IsInstanceOfType(xOrionEventManager.LogManager1, typeof(OrionHistoryFile));
        }// CreateOrionEventManager_OrionHistoryFile1_IsCreated()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void CreateOrionErrorManager_OrionHistoryFile2_IsCreated()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(null, OrionCoreTests.xOrionHistoryFile);

            Assert.IsNotNull(xOrionEventManager);
            Assert.IsInstanceOfType(xOrionEventManager.LogManager1, typeof(OrionHistoryFile));
        }// CreateOrionErrorManager_OrionHistoryFile2_IsCreated()
        #endregion

        #region Error reporting
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_Message_Reported()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;");

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
        }// EventReporting_Message_Reported()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_MessageException_Reported()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;", null, new OrionException("Test exception;"));

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
            Assert.IsInstanceOfType(xOrionEventManager.Log.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionEventManager.Log.SourceException.Message, "Test exception;");
        }// EventReporting_MessageException_Reported()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_MessageDisplayMessageException_Reported()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;", "Test display message;", new OrionException("Test exception;"));

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
            Assert.AreEqual(xOrionEventManager.Log.DisplayMessage, "Test display message;");
            Assert.IsInstanceOfType(xOrionEventManager.Log.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionEventManager.Log.SourceException.Message, "Test exception;");
        }// EventReporting_MessageDisplayMessageException_Reported()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_Reset_Reset()
        {
            OrionEventManager xOrionEventManager;

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;");
            xOrionEventManager.Reset();

            Assert.IsNull(xOrionEventManager.Log);
        }//EventReporting_Reset_Reset()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_Message_Sending_Warning()
        {
            OrionEventManager xOrionEventManager;

            Messenger.Default.Register<OrionMessageEventReporting>(this, (message) =>
            {
                Assert.AreEqual(message.Type, EventTypes.Warning);
            });

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;", "Test display message;", new OrionException("Test exception;"), EventTypes.Warning);

            Messenger.Default.Unregister<OrionMessageEventReporting>(this);

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
            Assert.AreEqual(xOrionEventManager.Log.DisplayMessage, "Test display message;");
            Assert.IsInstanceOfType(xOrionEventManager.Log.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionEventManager.Log.SourceException.Message, "Test exception;");
        }// EventReporting_Message_Sending_Warning()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_Message_Sending_Error()
        {
            OrionEventManager xOrionEventManager;

            Messenger.Default.Register<OrionMessageEventReporting>(this, (message) =>
            {
                Assert.AreEqual(message.Type, EventTypes.Error);
            });

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;", "Test display message;", new OrionException("Test exception;"), EventTypes.Error);

            Messenger.Default.Unregister<OrionMessageEventReporting>(this);

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
            Assert.AreEqual(xOrionEventManager.Log.DisplayMessage, "Test display message;");
            Assert.IsInstanceOfType(xOrionEventManager.Log.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionEventManager.Log.SourceException.Message, "Test exception;");
        }// EventReporting_Message_Sending_Error()
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void EventReporting_Message_Sending_Critical()
        {
            OrionEventManager xOrionEventManager;

            Messenger.Default.Register<OrionMessageEventReporting>(this, (message) =>
            {
                Assert.AreEqual(message.Type, EventTypes.Critical);
            });

            xOrionEventManager = new OrionEventManager(OrionCoreTests.xOrionHistoryFile);
            xOrionEventManager.ReportEvent("Test Message;", "Test display message;", new OrionException("Test exception;"), EventTypes.Critical);

            Messenger.Default.Unregister<OrionMessageEventReporting>(this);

            Assert.IsNotNull(xOrionEventManager.Log);
            Assert.AreEqual(xOrionEventManager.Log.LogMessage, "Test Message;");
            Assert.AreEqual(xOrionEventManager.Log.DisplayMessage, "Test display message;");
            Assert.IsInstanceOfType(xOrionEventManager.Log.SourceException, typeof(OrionException));
            Assert.AreEqual(xOrionEventManager.Log.SourceException.Message, "Test exception;");
        }// EventReporting_Message_Sending_Critical()
        #endregion

        #region Miscellaneous
        [TestCategory("OrionEventManager")]
        [TestMethod]
        public void ParseStackTrace_Parse_Parsed()
        {
            Boolean bWellFormattedLines;
            Collection<String> strResultats;

            bWellFormattedLines = true;

            strResultats = OrionLogInfos.ParseStackTrace();
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