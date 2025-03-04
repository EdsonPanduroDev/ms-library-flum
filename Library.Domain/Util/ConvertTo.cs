using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Domain.Util
{
    public static class ConvertTo
    {
        public static string Xml(Dictionary<string, object> parameters)
        {
            XElement xElement = new XElement("Record", parameters.Select((KeyValuePair<string, object> kv) => new XElement(kv.Key.Trim().ToLower(), kv.Value.ToString().Trim())));
            return xElement.ToString().Replace("'", "''");
        }
    }
}
