using System.Collections.Generic;

namespace LogamDev.Hearthstone.Services
{
    public class ValidationResult
    {
        public bool IsOk { get; set; }
        public List<string> Messages { get; set; }
    }
}
