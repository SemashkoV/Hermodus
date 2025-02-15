﻿
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Hermodus.UI.Models
{
    public class PostViewModel
    {
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Page> Pages { get; set; }
        public IEnumerable<Watch> Watches { get; set; }
        public IEnumerable<Company> Companies { get; set; }

        public Page Page { get; set; }
        public Post Post { get; set; }
       virtual public IEnumerable<User> UserDetails{get;set;}

      
        public PagingInfo PagingInfo { get; set; }
        public int? CurrentCategory { get; set; }
        public string CurrentTag { get; set; }
        public string HomeImageText { get; set; }
        public int HomeImage1 { get; set; }
        public int HomeImage2 { get; set; }
        public int HomeImage3 { get; set; }

        public string DisplayLastCategory { get; set; }
        public string DisplayLastPost { get; set; }
        public string DisplayGoogleAdv { get; set; }
        public string Search { get; set; }

    }
}