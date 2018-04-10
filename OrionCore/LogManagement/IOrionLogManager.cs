using System;

namespace OrionCore.LogManagement
{
    /// <summary>
    /// Interface implemented by some Orion classes used to store log informations in files or databases.
    /// </summary>
    public interface IOrionLogManager
    {
        #region Methods
        Boolean LogError(OrionLogInfos errorLog);
        #endregion
    }
}
