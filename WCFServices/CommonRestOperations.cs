using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    public class CommonRestOperations : ICommonRestOperations
    {
        public string getCurrentDate()
        {
            string date = DateTime.Now.ToString();
            return date;
        }


    }
}
