
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
  public  interface IImageRepository
    {
        bool Save(Image image);
        IEnumerable<Image> ImageIEnum { get; }
        IQueryable<Image> ImageList { get; }
        Image Delete(int? Id);
        Image Details(int? Id);

    }
}
