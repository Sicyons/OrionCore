using GalaSoft.MvvmLight.Messaging;

namespace OrionCore.EventManagement
{
    public class OrionMessageEventReporting : MessageBase
    {
        #region Properties
        public EventTypes Type { get; private set; }
        #endregion

        #region Constructors
        public OrionMessageEventReporting()
            : this(EventTypes.Information)
        {
        }// OrionMessageEventReporting()
        public OrionMessageEventReporting(EventTypes type)
        {
            this.Type = type;
        }// OrionMessageEventReporting()
        #endregion
    }
}