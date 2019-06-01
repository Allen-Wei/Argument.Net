using System;
using System.Linq;
using System.Collections.Generic;

namespace Argument.Net
{
    public static class Util
    {
        /// <summary>
        /// join multi values by seperator
        /// </summary>
        public static String StringJoin<T>(this IEnumerable<T> values, String seperator) => values == null ? null : String.Join(seperator, values);
        public static (bool, String) GetArgValue(List<String> args, String argName)
        {
            var lowerCaseArgs = args.Select(arg => (arg ?? "").ToLower()).ToList();
            var lowerCaseArgName = argName.ToLower();

            var index = lowerCaseArgs.IndexOf("-" + lowerCaseArgName);
            if (index == -1)
                index = lowerCaseArgs.IndexOf("--" + lowerCaseArgName);

            if (index == -1) return (false, "NOT_FOUND");
            var nextValue = index == args.Count - 1 ? "true" : args[index + 1];
            if (nextValue[0] == '-')
                return (true, "true");
            return (true, nextValue);
        }
    }
}
