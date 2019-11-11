using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website
{
    [Route("/form")]
    public class FormController : Controller
    {
        [HttpGet]
        [HttpPost]
        [Produces("text/html")]
        public IActionResult Index()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!doctype html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<link href='lib/bootstrap/dist/css/bootstrap.min.css' rel='stylesheet' />");
            sb.AppendLine("<link href='lib/bootstrap/dist/css/bootstrap-theme.css' rel='stylesheet' />");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");
            sb.AppendLine("<table class='table table-striped table-hover'>");
            if (HttpContext.Request.Query.Any())
            {
                sb.AppendLine("<tr class='success'><th colspan='2' class='alert alert-success'>Form GET</th></tr>");
                AddRow(sb, "name", "value", true);
                foreach (var kvp in HttpContext.Request.Query)
                {
                    AddRow(sb, kvp.Key, kvp.Value);
                }
            }
            else
            {
                sb.AppendLine("<tr class='warning'><th colspan='2' class='alert alert-warning'>Form GET is Empty.</th></tr>");
            }
            if (Request.HasFormContentType && Request.Form != null && Request.Form.Any())
            {
                sb.AppendLine("<tr class='success'><th colspan='2' class='alert alert-success'>Form POST</th></tr>");
                AddRow(sb, "name", "value", true);
                foreach (var kvp in Request.Form)
                {
                    AddRow(sb, kvp.Key, kvp.Value);
                }
            }
            else
            {
                sb.AppendLine("<tr class='warning'><th colspan='2' class='alert alert-warning'>Form POST is Empty.</th></tr>");
            }
            sb.AppendLine("</table>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return Ok(sb.ToString());
        }

        private static void AddRow(StringBuilder sb, string name, StringValues value, bool isHeader = false)
        {
            string tag = isHeader ? "th" : "td";
            sb.Append("<tr>");
            sb.AppendFormat("<{0}>{1}</{0}>", tag, name);
            if (value.Count > 1)
            {
                sb.AppendFormat("<{0}><ol start='0'>", tag);
                foreach (var val in value)
                {
                    sb.AppendFormat("<li>{0}</li>", val);
                }
                sb.AppendFormat("</ol></{0}>", tag);
            }
            else
            {
                sb.AppendFormat("<{0}>{1}</{0}>", tag, value);
            }
            sb.AppendLine("</tr>");
        }
    }
}
