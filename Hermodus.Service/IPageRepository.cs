using Hermodus.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
  public  interface IPageRepository
    {
        IEnumerable<Page> LastPage { get; }
        void Save(Page page);
        IEnumerable<Page> PageIEnum { get; }
        List<Page> PageList { get; }
        Page Details(int? Id);
        Page Delete(int? Id);

    }
}
