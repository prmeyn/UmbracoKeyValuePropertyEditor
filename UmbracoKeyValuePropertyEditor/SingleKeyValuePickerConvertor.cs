using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

namespace UmbracoKeyValuePropertyEditor
{
	public class SingleKeyValuePickerConvertor : IPropertyValueConverter
	{
		public bool IsConverter(IPublishedPropertyType propertyType) => propertyType.EditorAlias == "SingleKeyValuePicker";
		public object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) => inter as string;

		public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
		{
			throw new NotImplementedException();
		}

		public object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) => source as string;

		public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.Element;

		public Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(string);

		public bool? IsValue(object value, PropertyValueLevel level) => string.IsNullOrWhiteSpace(value as string);
	}
}
