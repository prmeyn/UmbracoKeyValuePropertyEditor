# UmbracoKeyValuePropertyEditor

UmbracoKeyValuePropertyEditor property editor for Umbraco

This installs a custom property editor that can be used to configure external data to Umbraco nodes

After installing this, you can add inherit from `KeyValueUmbracoPropertyEditorController` to implement an api endpoint that can server as a data source, for example: to create the following API endpoint `/umbraco/backoffice/Sample/LanguageDemoApi` here is the [sample code](https://github.com/prmeyn/UmbracoLanguagePicker/blob/main/UmbracoLanguagePicker/LanguageApiController.cs) needed.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;
using UmbracoKeyValuePropertyEditor;

namespace ExternalApiPickerDemo.Core.Demo
{

    [PluginController("UmbracoLanguagePicker")]
    public sealed class LanguageApiController : KeyValueUmbracoPropertyEditorController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly ILocalizationService _localizationService;

        public LanguageApiController(UmbracoHelper umbracoHelper, ILocalizationService localizationService)
        {
            _umbracoHelper = umbracoHelper;
            _localizationService = localizationService;
        }

        public override IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(string nodeIdOrGuid, string propertyAlias, bool uniqueFilter, bool allowNull)
        {
             try
            {
                string[] usedUpLanguageCodes = Array.Empty<string>();
                try
                {
                    // Current node block
                    IPublishedContent currentNode = null;
                    if (int.TryParse(nodeIdOrGuid, out int nodeId) && nodeId > 0)
                    {
                        currentNode = _umbracoHelper.Content(nodeId);
                    }
                    else if (Guid.TryParse(nodeIdOrGuid, out Guid Key))
                    {
                        currentNode = _umbracoHelper.Content(Key);
                    }
                    
                    // Parent node block
                    IPublishedContent parentNode = null;
                    if (int.TryParse(parentNodeIdOrGuid, out int parentNodeId) && parentNodeId > 0)
                    {
                        parentNode = _umbracoHelper.Content(parentNodeId);
                    }
                    else if (Guid.TryParse(parentNodeIdOrGuid, out Guid Key))
                    {
                        parentNode = _umbracoHelper.Content(Key);
                    }
                    usedUpLanguageCodes = GetValuesOfChildrensProperty(parentNode, propertyAlias, currentNode?.Id).ToArray();
                }
                catch { uniqueFilter = false; }
                
                LanguageDTO[] languageList = null;
                if (uniqueFilter)
                {
                    languageList = (new LanguageApiWrapper(_localizationService)).AllLanguages.Where(c => !usedUpLanguageCodes.Contains(c.ISOCode.ToLowerInvariant())).ToArray();
                }
                else
                {
                    languageList = (new LanguageApiWrapper(_localizationService)).AllLanguages.ToArray();
                }
                if (allowNull)
                {
                    languageList = languageList.Prepend(new LanguageDTO { ISOCode = "", EnglishName = "" }).ToArray();
                }
                return languageList.ToDictionary(c => c.ISOCode.ToLowerInvariant(), c => "").OrderBy(v => v.Key);
            }
            catch
            {
                return null;
            }
        }

        private IEnumerable<string> GetValuesOfChildrensProperty(IPublishedContent node, string propertyAlias, int nodeId)
        {
            var nodes = node == null ? _umbracoHelper.ContentAtRoot() : node.Children;
            return nodes.Where(c => c.Id != nodeId).Select(c => c.Value<string>(propertyAlias)?.ToLowerInvariant());
        }
    }
}
```

```csharp
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Services;

namespace ExternalApiPickerDemo.Core.Demo
{
	public class LanguageApiWrapper
	{
		private readonly ILocalizationService _localizationService;

		public LanguageApiWrapper(ILocalizationService localizationService)
		{
			_localizationService = localizationService;
		}

		public IEnumerable<LanguageDTO> AllLanguages => _localizationService.GetAllLanguages().Select(l => new LanguageDTO() { ISOCode = l.IsoCode, EnglishName = l.CultureName });
	}
}
```

```csharp
namespace ExternalApiPickerDemo.Core.Demo
{
	public class LanguageDTO
	{
		public string ISOCode { get; set; }
		public string EnglishName { get; set; }
	}
}

```
