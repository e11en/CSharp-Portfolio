using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ManagerResponse
    {
        public int ItemIdentifier { get; set; }

        public bool Approved { get; set; }

        public Manager Manager { get; set; }
    }
}
