using System;
using System.Collections.Generic;

namespace Argument.Net
{
    [AttributeUsage(System.AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ArgumentAttribute : System.Attribute
    {
        /// <summary>
        /// Argument Name
        /// </summary>
        public String Name { get; set; }
        public String Alias { get; set; }
        public bool Required { get; set; }
        public String DefaultValue { get; set; }
        public IEnumerable<String> AvaliableValues { get; set; }
    }
}