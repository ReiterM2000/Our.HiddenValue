using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace Our.HiddenValue
{
    public class ContentTypeSavedHandler : INotificationHandler<ContentTypeSavedNotification>
    {
        private readonly IUmbracoContextFactory _factory;

        private readonly IContentService _service;

        private readonly IContentTypeService _contentTypeService;

        public ContentTypeSavedHandler(IUmbracoContextFactory factory, IContentService service, IContentTypeService contentTypeService)
        {
            _factory = factory;
            _service = service;
            _contentTypeService = contentTypeService;
        }

        public void Handle(ContentTypeSavedNotification notification)
        {
            foreach (var contentType in notification.SavedEntities)
            {
                List<IPropertyType> props = contentType.CompositionPropertyTypes.Where(x => x.PropertyEditorAlias == Constants.PropertyAlias)?.ToList();
                if(props != null && props.Any())
                {
                    List<IContentType> types = _contentTypeService.GetAll().Where(x => x.CompositionPropertyTypes.Any(x => x.PropertyEditorAlias == Constants.PropertyAlias))?.ToList();
                    if (types != null && types.Any())
                    {
                        using (var context = _factory.EnsureUmbracoContext())
                        {
                            foreach (var type in types)
                        {

                                var publishedContent = context.UmbracoContext.Content.GetByXPath($"//{type.Alias}");
                                if (publishedContent != null && publishedContent.Any())
                                {
                                    var content = _service.GetByIds(publishedContent.Select(x => x.Id));
                                    content.Where(x => x.Published)?.ToList()?.ForEach(x => _service.SaveAndPublish(x));
                                    content.Where(x => !x.Published)?.ToList()?.ForEach(x => _service.Save(x));
                                }
                            }
                        }
                    }


                }
            }
        }
    }
}
