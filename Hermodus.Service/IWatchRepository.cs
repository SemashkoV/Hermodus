
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
  public interface IWatchRepository
    {
        bool Save(Watch watch);
        IEnumerable<Watch> WatchIEnum { get; }
        IQueryable<Watch> WatchList { get; }
        Watch Delete(int? Id);
        Watch Details(int? Id);

    }
}
