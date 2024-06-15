using NetCad.Entity;
using System.Collections.Generic;
using System.Text;

namespace NetCad.Services.Extensions
{
    public static class ListExtensions
    {
        public static string ToHtmlTable(this List<Student> students)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr><th>FirstName</th><th>LastName</th><th>BirthDate</th><th>PlaceOfBirth</th><th>RegistrationDateTime</th></tr>");
            foreach (var student in students)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", student.FirstName);
                sb.AppendFormat("<td>{0}</td>", student.LastName);
                sb.AppendFormat("<td>{0:yyyy-MM-dd}</td>", student.BirthDate);
                sb.AppendFormat("<td>{0}</td>", student.PlaceOfBirth);
                sb.AppendFormat("<td>{0:yyyy-MM-dd HH:mm:ss}</td>", student.RegistrationDateTime);
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
