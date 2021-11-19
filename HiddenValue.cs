using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Our.HiddenValue
{
    /// <summary>
    /// Composer for registering the notification handler
    /// </summary>
    public class HiddenValue : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.PropertyValueConverters().Append(typeof(HiddenValuePropertyValueConverter));
            builder.AddNotificationHandler<SendingContentNotification, HideHiddenValue>();
            builder.AddNotificationHandler<ContentSavingNotification, SaveHiddenValue>();
            builder.AddNotificationHandler<ContentTypeSavedNotification, ContentTypeSavedHandler>();
        }
    }
}
