using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    public class Service : IService
    {
        public string Echo(string text)
        {
            return text;
        }
    }
}
