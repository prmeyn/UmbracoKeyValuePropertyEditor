using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Api.Management.Controllers;

namespace UmbracoKeyValuePropertyEditor
{
	public abstract class KeyValueUmbracoPropertyEditorController : ManagementApiControllerBase
	{
		public abstract IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(string nodeIdOrGuid, string propertyAlias, int uniqueFilter, int allowNull);
	}
}
