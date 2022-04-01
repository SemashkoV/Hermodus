
using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
  public  interface ITextRepository
    {
        bool Save(Text text);
        IEnumerable<Text> TextIEnum { get; }
        IQueryable<Text> TextList { get; }
        Text Delete(int? Id);
        Text Details(int? Id);

    }
}
