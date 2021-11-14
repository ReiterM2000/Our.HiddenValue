using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;

namespace Our.HiddenInput
{
    /// <summary>
    /// Notificationhandler for the ContentSavingNotification
    /// </summary>
    public class SaveHiddenInput : INotificationHandler<ContentSavingNotification>
    {
        private readonly IDataTypeService _service;

        public SaveHiddenInput(IDataTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Checking if the content contains a property of the hiddeninput propertytype
        /// if one is found the datatype is is gotten and based on the configuration the value is set.
        /// </summary>
        /// <param name="notification">Value of the ContentSavingNotification</param>
        public void Handle(ContentSavingNotification notification)
        {
            foreach (var content in notification.SavedEntities)
            {
                IEnumerable<IProperty> hiddenProperties = content.Properties.Where(x => x.PropertyType.PropertyEditorAlias == Constants.PropertyAlias);
                if (hiddenProperties.Any())
                {
                    foreach (var prop in hiddenProperties)
                    {
                        IDataType type = _service.GetDataType(prop.PropertyType.DataTypeId);
                        Dictionary<string, object> hiddenInput = type.Configuration as Dictionary<string, object>;
                        if(hiddenInput != null)
                        {
                            prop.SetValue(hiddenInput[Constants.ConfigurationAlias]);
                        }
                    }
                }
            }
        }
    }
}
