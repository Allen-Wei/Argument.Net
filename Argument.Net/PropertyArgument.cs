using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Argument.Net
{
    public class PropertyArgument
    {
        public PropertyInfo Property { get; set; }
        public ArgumentAttribute Argument { get; set; }
        public IEnumerable<String> ArgNames
        {
            get
            {
                return new[] { this.Argument.Name, this.Argument.Alias, this.Property.Name }.Where(n => !String.IsNullOrWhiteSpace(n));
            }
        }
        public bool SetPropValue<T>(List<String> args, T instance)
        {
            var argValues = this.ArgNames
            .Select(n => Util.GetArgValue(args, n))
            .Where(item => item.Item1 && !String.IsNullOrWhiteSpace(item.Item2))
            .Select(item => item.Item2)
            .ToList();

            if (this.Argument.DefaultValue != null)
                argValues.Add(this.Argument.DefaultValue);

            if (argValues.Count == 0) return false;

            String argVal = argValues.FirstOrDefault();

            try
            {
                Object value = Convert.ChangeType(argVal, this.Property.PropertyType);
                if (value == null) return false;
                this.Property.SetValue(instance, value, null);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
    }
}