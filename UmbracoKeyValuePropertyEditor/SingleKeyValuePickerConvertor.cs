using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

namespace UmbracoKeyValuePropertyEditor
{
	public class SingleKeyValuePickerConvertor : IPropertyValueConverter
	{
		public object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
		{
			throw new NotImplementedException();
		}

		public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
		{
			throw new NotImplementedException();
		}

		public object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview)
		{
			throw new NotImplementedException();
		}

		public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
		{
			throw new NotImplementedException();
		}

		public Type GetPropertyValueType(IPublishedPropertyType propertyType)
		{
			throw new NotImplementedException();
		}

		public bool IsConverter(IPublishedPropertyType propertyType) => propertyType.EditorAlias == "SingleKeyValuePicker";

		public bool? IsValue(object value, PropertyValueLevel level)
		{
			throw new NotImplementedException();
		}
	}
}
