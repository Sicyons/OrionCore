using System;

namespace OrionCore.ErrorManagement
{
    /// <summary>
    /// Interface implemented by some Orion classes used to store error log informations in files or databases.
    /// </summary>
    public interface IOrionErrorLogManager
    {
        #region Methods
        Boolean LogError(StructOrionErrorLogInfos errorLog);
        #endregion
    }
}
