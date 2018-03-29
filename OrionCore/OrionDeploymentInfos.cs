using System;
using System.IO;
using System.Deployment.Application;

namespace OrionCore
{
    static public class OrionDeploymentInfos
    {
        #region Properties
        static public String DataFolder
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed == false ? AppDomain.CurrentDomain.BaseDirectory : ApplicationDeployment.CurrentDeployment.DataDirectory;
            }
        }
        static public String UpdateFolder
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed == false || ApplicationDeployment.CurrentDeployment == null || ApplicationDeployment.CurrentDeployment.UpdateLocation == null ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updates") : ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsolutePath;
            }
        }
        static public String ApplicationVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        static public String PublicationVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed == true ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : String.Empty;
            }
        }
        #endregion
    }
}
