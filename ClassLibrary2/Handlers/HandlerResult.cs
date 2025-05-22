using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Handlers
{
    public class HandlerResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static HandlerResult Ok() => new HandlerResult { Success = true };
        public static HandlerResult Fail(string message) => new HandlerResult { Success = false, ErrorMessage = message };
    }
}
