using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.SAP.Helpers
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return a.Trim().ToLowerInvariant().Equals(b.Trim().ToLowerInvariant());
        }
    }
}
