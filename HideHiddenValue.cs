using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Notifications;

namespace Our.HiddenValue
{
    /// <summary>
    /// Notificationhandler for the SendingContentNotification
    /// </summary>
    public class HideHiddenValue : INotificationHandler<SendingContentNotification>
    {
        /// <summary>
        /// Looks through the content variations and removese the properties which are based on the HiddenValue type and then hides them
        /// </summary>
        /// <param name="notification">the sendingcontentnotification value</param>
        public void Handle(SendingContentNotification notification)
        {
            foreach (var variant in notification.Content.Variants)
            {
                foreach (var tab in variant.Tabs)
                {
                    IEnumerable<ContentPropertyDisplay> properties = tab.Properties.Where(x => x.Editor != Constants.PropertyAlias);
                    tab.Properties = properties;
                }
            }
        }
    }
}
