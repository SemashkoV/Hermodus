﻿
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hermodus.UI.Models
{
    public class CategoryLVM
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentCategory { get; set; }
    }
}