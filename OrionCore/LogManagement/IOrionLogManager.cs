using System;

namespace OrionCore.LogManagement
{
    /// <summary>
    /// Interface implemented by some Orion classes used to save log informations in files or databases.
    /// </summary>
    public interface IOrionLogManager
    {
        #region Methods
        Boolean SaveLog(OrionLogInfos errorLog);
        #endregion
    }
}
