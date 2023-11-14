using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public enum StatosCode
    {
        Success = 1000,
        Error,
        Warning
    }
    public class ResponseDTO
    {
        public StatosCode Status { get; set; }
        public string StatusText { get; set; }
    }
}
