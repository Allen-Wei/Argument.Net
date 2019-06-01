using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Argument.Net
{
    public class ArgumentBuilder<T> where T : new()
    {
        private readonly List<PropertyArgument> Properties;
        private readonly List<String> Args;
        public ArgumentBuilder(IEnumerable<String> args)
        {
            this.Properties = typeof(T).GetProperties()
            .Where(p => p.CanWrite)
            .Select(p => new PropertyArgument { Argument = p.GetCustomAttribute<ArgumentAttribute>(), Property = p })
            .Where(item => item.Argument != null)
            .ToList();

            this.Args = args.ToList();
        }
        public T GetArgs()
        {
            var instance = new T();
            this.Properties.ForEach(pa => pa.SetPropValue(this.Args, instance));
            return instance;
        }
        public (bool, T, IEnumerable<string>) GetValidArgs()
        {
            var instance = new T();
            var propArgs = this.Properties.Select(pa => new { Settled = pa.SetPropValue(this.Args, instance), PropArg = pa }).ToList();

            IEnumerable<String> argInvalidErrors1 = from pa in propArgs
                                                    where !pa.Settled
                                                    let errors = new List<String>()
                                                    select "";
            List<String> argInvalidErrors = propArgs.Where(pa => !pa.Settled).Select(pa =>
            {
                List<String> errors = new List<string>();
                if (pa.PropArg.Argument.Required)
                    errors.Add("can't be null");
                if (pa.PropArg.Argument.AvaliableValues != null)
                    errors.Add($"values must in ({pa.PropArg.Argument.AvaliableValues.StringJoin(", ")})");

                return $"Argument ({pa.PropArg.ArgNames.StringJoin(",")}) {errors.StringJoin(" and ")}";
            }).ToList();
            if (argInvalidErrors.Count > 0)
                return (false, instance, argInvalidErrors);

            return (true, instance, new List<String>());
        }
    }
}