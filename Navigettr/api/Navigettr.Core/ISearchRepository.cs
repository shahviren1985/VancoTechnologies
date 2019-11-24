using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
  public interface ISearchRepository
    {
        int CheckServiceType(string serviceType);

    }
}
