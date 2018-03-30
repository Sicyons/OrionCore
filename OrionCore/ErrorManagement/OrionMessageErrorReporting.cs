using GalaSoft.MvvmLight.Messaging;

namespace OrionCore.ErrorManagement
{
    public class OrionMessageErrorReporting : MessageBase
    {
        #region Properties
        public ErrorTypes Type { get; private set; }
        #endregion

        #region Constructors
        public OrionMessageErrorReporting()
            : this(ErrorTypes.Error)
        {
        }// OrionMessageErrorReporting()
        public OrionMessageErrorReporting(ErrorTypes type)
        {
            this.Type = type;
        }// OrionMessageErrorReporting()
        #endregion
    }
}