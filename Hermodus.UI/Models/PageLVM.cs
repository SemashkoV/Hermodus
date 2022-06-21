
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hermodus.UI.Models
{
    public class PageLVM
    {
        public IEnumerable<Page> Pages { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentPage { get; set; }
    }
}