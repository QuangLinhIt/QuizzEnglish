using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Core.CustomerError
{
    public class CustomeNotFoundException : Exception
    {
        public CustomeNotFoundException(string strMessage) : base(strMessage)
        {
        }
    }
}
