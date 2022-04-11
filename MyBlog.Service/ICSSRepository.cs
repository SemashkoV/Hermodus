
using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
  public  interface ICSSRepository
    {
        bool Save(CSS css);
        IEnumerable<CSS> CSSIEnum { get; }
        IQueryable<CSS> CSSList { get; }
        CSS Delete(int? Id);
        CSS Details(int? Id);

    }
}
