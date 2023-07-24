using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Graphql_example_code.Application.Core.Results
{
    public class ResultT<T> : Result
    {
        private readonly T _value;
        public ResultT(T value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
            => _value = value;
        public T Value => IsSuccess
                        ? _value
                        : _value;
    }
}
