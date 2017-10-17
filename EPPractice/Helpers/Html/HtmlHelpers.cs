using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EPPractice.Helpers.Html
{
    public static class HtmlHelpers
    {
        public static IHtmlString CheckBoxList(this HtmlHelper helper, IEnumerable<SelectListItem> items)
        {
            var output = new StringBuilder();
            foreach (var item in items.ToList())
            {
                output.AppendFormat("<input name='{0}' {1}  type='checkbox'>", item.Value, item.Selected ? "Checked" : "");
                output.AppendFormat("<label>{0}</label>", item.Text);
            }
            return new HtmlString(output.ToString());
        }

        public static IHtmlString CheckBoxList(this HtmlHelper helper, List<SelectListItem> items, string jQueryEvents)
        {
            var output = new StringBuilder();
            foreach (var item in items.ToList())
            {
                output.AppendFormat("<input name='{0}' value = '{0}' {1}  type='checkbox' {2} />", item.Value, item.Selected ? "Checked" : "", jQueryEvents);
                output.AppendFormat("&nbsp;<label>{0}</label>&nbsp;", item.Text);
            }
            return new HtmlString(output.ToString());
        }
    }
}