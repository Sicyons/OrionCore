using System;
using GalaSoft.MvvmLight;

namespace OrionCore.ErrorManagement
{
    public class OrionErrorManager
    {
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
        }// OrionErrorManager()
        #endregion
    }
}
