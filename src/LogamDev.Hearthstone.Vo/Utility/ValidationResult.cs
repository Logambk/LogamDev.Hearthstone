using System.Collections.Generic;

namespace LogamDev.Hearthstone.Vo.Utility
{
    public class ValidationResult
    {
        public bool IsOk { get; set; }
        public List<string> Messages { get; set; }
    }
}
