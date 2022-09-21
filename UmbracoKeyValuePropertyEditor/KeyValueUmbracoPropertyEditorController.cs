using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace Umbraco.KeyValuePropertyEditor
{
	[ValidateAngularAntiForgeryToken]
	public abstract class KeyValueUmbracoPropertyEditorController : UmbracoAuthorizedJsonController
	{
		public abstract IOrderedEnumerable<KeyValuePair<string, string>> GetKeyValueList(int nodeId, string propertyAlias, int uniqueFilter, int allowNull);
	}
}
