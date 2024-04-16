using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace UmbracoKeyValuePropertyEditor
{
	[ValidateAngularAntiForgeryToken]
	public abstract class KeyValueUmbracoPropertyEditorController : UmbracoAuthorizedJsonController
	{
		public abstract IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(string nodeIdOrGuid, string propertyAlias, int uniqueFilter, int allowNull);
	}
}
