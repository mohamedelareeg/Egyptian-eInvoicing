using System.Web;

namespace EgyptianeInvoicing.MVC.Constants
{
    public class QueryStringBuilder
    {
        private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

        public QueryStringBuilder Add(string key, object value)
        {
            if (value != null)
            {
                var valueString = value.ToString();
                if (!string.IsNullOrEmpty(valueString))
                {
                    _parameters[key] = valueString;
                }
            }
            return this;
        }

        public override string ToString()
        {
            if (_parameters.Count == 0)
                return string.Empty;

            var queryString = string.Join("&", _parameters
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                .Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

            return $"?{queryString}";
        }
    }
}
