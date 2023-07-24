using FluentValidation.Results;
using Graphql_example_code.Application.Core.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Graphql_example_code.Application.Core
{
        public static class ToResultErrorExtenstion
        {
            public static List<string> ToResultError(this List<ValidationFailure> errors)
            {
                var result = new List<string>();
                foreach (var error in errors)
                {
                    result.Add(new(error.ErrorMessage));
                }

                return result;
            }
        }
        public static class GetDescriptionExtenstion
        {
            public static string GetDescription(this Enum value)
            {
                Type type = value.GetType();
                string name = Enum.GetName(type, value);
                if (name is not null)
                {
                    FieldInfo field = type.GetField(name);
                    if (field is not null)
                    {
                        DescriptionAttribute attr =
                               Attribute.GetCustomAttribute(field,
                                 typeof(DescriptionAttribute)) as DescriptionAttribute;
                        if (attr is not null)
                        {
                            return attr.Description;
                        }
                    }
                }
                return null;
            }
        }
           
    
}
