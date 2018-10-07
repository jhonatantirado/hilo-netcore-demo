using System;
using System.Text;

namespace EnterprisePatterns.Api.Common
{
    public class Util
    {
        public static String getTableName(String value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in value)
            {
                if (Char.IsUpper(c) && builder.Length > 0)
                {
                    builder.Append('_');
                }
                builder.Append(c);
            }
            return builder.ToString().ToLower();
        }
    }
}
