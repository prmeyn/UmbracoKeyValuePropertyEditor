# UmbracoKeyValuePropertyEditor

UmbracoKeyValuePropertyEditor property editor for Umbraco

This installs a custom property editor that can be used to configure external data to Umbraco nodes

After installing this, you can add inherit from `KeyValueUmbracoPropertyEditorController` to implement an api endpoint that can server as a data source, for example: to create the following API endpoint `/umbraco/backoffice/Sample/LanguageDemoApi` here is the sample code needed.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;
using Umbraco.KeyValuePropertyEditor;

namespace ExternalApiPickerDemo.Core.Demo
{
	[PluginController("Sample")]
	public class LanguageDemoApiController : KeyValueUmbracoPropertyEditorController
	{
		private readonly UmbracoHelper _umbracoHelper;
		private readonly ILocalizationService _localizationService;

		public LanguageDemoApiController(UmbracoHelper umbracoHelper, ILocalizationService localizationService)
		{
			_umbracoHelper = umbracoHelper;
			_localizationService = localizationService;
		}
			
		public override IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(int nodeId, string propertyAlias, int uniqueFilter = 0, int allowNull = 0)
		{
			try
			{
				string[] usedUpLanguageCodes = Array.Empty<string>();
				try {
					var parent = _umbracoHelper.Content(nodeId).Parent;
					usedUpLanguageCodes = (parent == null ? _umbracoHelper.Content(nodeId).Children.Where(c => c.Id != nodeId).Select(c => c.Value<string>(propertyAlias)?.ToLowerInvariant()) : parent.Children.Where(c => c.Id != nodeId).Select(c => c.Value<string>(propertyAlias)?.ToLowerInvariant()).Union(_umbracoHelper.Content(nodeId).Children.Where(c => c.Id != nodeId).Select(c => c.Value<string>(propertyAlias)?.ToLowerInvariant()))).ToArray();
				} catch { uniqueFilter = 0; }
				LanguageDTO[] languageList = null;
				if (uniqueFilter == 1)
				{
					languageList = (new LanguageApiWrapper(_localizationService)).AllLanguages.Where(c => !usedUpLanguageCodes.Contains(c.ISOCode.ToLowerInvariant())).ToArray();
				}
				else
				{
					languageList = (new LanguageApiWrapper(_localizationService)).AllLanguages.ToArray();
				}
				if (allowNull == 1)
				{
					languageList = languageList.Prepend(new LanguageDTO { ISOCode = "", EnglishName = "NONE" }).ToArray();
				}
				return languageList.ToDictionary(c => c.ISOCode.ToLowerInvariant(), c => c.EnglishName).OrderBy(v => v.Value);
			}
			catch
			{
				return null;
			}
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
