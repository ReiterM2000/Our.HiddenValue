using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Our.HiddenInput
{
    /// <summary>
    /// Composer for registering the notification handler
    /// </summary>
    public class HiddenInputComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.PropertyValueConverters().Append(typeof(HiddenInputPropertyValueConverter));
            builder.AddNotificationHandler<SendingContentNotification, HideHiddenInput>();
            builder.AddNotificationHandler<ContentSavingNotification, SaveHiddenInput>();
            builder.AddNotificationHandler<ContentTypeSavedNotification, ContentTypeSavedHandler>();
        }
    }
}
