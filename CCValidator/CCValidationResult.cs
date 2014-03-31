using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCValidator
{
    public class CCValidationResult
    {
        public string CardNumber { get; internal set; }
        public bool InValid { get; internal set; }
        public CardType CardType { get; internal set; }
    }
}
