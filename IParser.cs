using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder
{
    interface IParser
    {
        IEnumerable<DocStruct> GetDocs();
    }
}
