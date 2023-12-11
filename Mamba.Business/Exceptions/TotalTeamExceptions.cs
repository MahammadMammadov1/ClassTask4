using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Exceptions
{
    public class TotalTeamExceptions : Exception
    {
        public string Msg { get; set; }
        public TotalTeamExceptions()
        {
        }

        public TotalTeamExceptions(string msg,string? message) : base(message)
        {
        }
    }
}
