using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCValidator
{
    class AsyncStateObject
    {
        public AsyncStateObject(Action<List<CCValidationResult>> callback, List<CCValidationResult> results)
        {
            this.UserCallback = callback;
            this.Results = results;
        }
        public Action<List<CCValidationResult>> UserCallback {get; set;}
        public List<CCValidationResult> Results { get; set; }

    }
}
