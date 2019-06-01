using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Argument.Net
{
    public class ArgumentBuilder
    {
        private ArgumentBuilder() { }
        public static ArgumentBuilder<T> Build<T>(IEnumerable<String> args)
        where T : new()
        {
            return new ArgumentBuilder<T>(args);
        }
    }
}