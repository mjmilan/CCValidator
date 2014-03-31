using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCValidator
{
    class AsyncValidatorThreadStartParams
    {
        public object State { get; set; }
        public IEnumerable<string> CardNumbers { get; set; }
        public Action<CCValidator, Object, IList<CCValidationResult>> Callback { get; set; }
        public System.Threading.Thread CallingThread { get; set; }
    }
}
