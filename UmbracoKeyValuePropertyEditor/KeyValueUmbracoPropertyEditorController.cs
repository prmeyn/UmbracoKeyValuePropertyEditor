using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Api.Management.Controllers;

namespace UmbracoKeyValuePropertyEditor
{
	public abstract class KeyValueUmbracoPropertyEditorController : ManagementApiControllerBase
	{
		public abstract IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(string parentNodeIdOrGuid, string nodeIdOrGuid, string propertyAlias, bool uniqueFilter, bool allowNull);
	}
}
