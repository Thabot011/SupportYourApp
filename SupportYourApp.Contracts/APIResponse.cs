using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class APIResponse<T>
    {
        public string Message { get; set; }
        public T Response { get; set; }
    }
}
