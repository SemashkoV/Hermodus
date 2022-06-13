using MyBlog.Data;
using MyBlog.Service;
using MyBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using X.PagedList;
using Microsoft.AspNet;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace MyBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IWatchRepository watchRepository;
        private readonly ISettingRepository repositorySetting;

        public HomeController(IPostRepository PostRepo,ISettingRepository repoSetting, IWatchRepository WatchRepo)
        {
               postRepository=PostRepo;
            
               repositorySetting = repoSetting;
               watchRepository=WatchRepo;
        }
        // GET: Home
        public ActionResult Index(int? page, SortState? sortOrder)
        {
            Setting _HomePageSetting;
            _HomePageSetting = repositorySetting.GetSetting;

            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.AgeSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewBag.CompSort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
         
            int PageSize = 4;
            int PagePostSize = 3;
            // _HomePageSetting.PostNumberInPage;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    PostViewModel model = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderBy(p => p.Id)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model);
                case SortState.PriceAsc:
                    PostViewModel model1 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderBy(p => p.Price)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model1);
                case SortState.PriceDesc:
                    PostViewModel model2 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderByDescending(p => p.Price)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model2);
                case SortState.CompanyAsc:
                    PostViewModel model3 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderBy(p => p.CompanyId)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model3);
                case SortState.CompanyDesc:
                    PostViewModel model4 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderByDescending(p => p.CompanyId)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model4);
                default:
                    PostViewModel model5 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PagePostSize),

                        Watches = watchRepository.WatchList
                        .OrderByDescending(p => p.Id)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model5);
            }

            
         
            
    }
        public ActionResult Catalog(int? page, SortState? sortOrder, string name="")
        {
            Setting _HomePageSetting;
            _HomePageSetting = repositorySetting.GetSetting;

            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.AgeSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewBag.CompSort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            ViewBag.SortOrder = sortOrder;
            int PageSize = _HomePageSetting.PostNumberInPage;
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    PostViewModel model = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderBy(p => p.Id)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model);
                case SortState.PriceAsc:
                    PostViewModel model1 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderBy(p => p.Price)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model1);
                case SortState.PriceDesc:
                    PostViewModel model2 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderByDescending(p => p.Price)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model2);
                case SortState.CompanyAsc:
                    PostViewModel model3 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderBy(p => p.CompanyId)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model3);
                case SortState.CompanyDesc:
                    PostViewModel model4 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderByDescending(p => p.CompanyId)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model4);
                default:
                    PostViewModel model5 = new PostViewModel
                    {
                        Posts = postRepository.PostList
                        .OrderBy(p => p.PostId)
                        .OrderByDescending(p => p.Create_time)
                        .ToPagedList(page ?? 1, PageSize),

                        Watches = watchRepository.WatchList
                        .Where(p => p.CompanyId.Contains(name))
                        .OrderByDescending(p => p.Id)
                        .ToPagedList(page ?? 1, PageSize),

                        HomeImageText = _HomePageSetting.HomeImageText,
                        HomeImage1 = _HomePageSetting.HomeImage1,
                        HomeImage2 = _HomePageSetting.HomeImage2,
                        HomeImage3 = _HomePageSetting.HomeImage3,
                    };
                    return View(model5);
            }

            
         
            
    }


    public ActionResult Feed()
        {
            IEnumerable<Post> posts = postRepository.PostIEnum.OrderByDescending(c => c.Create_time).Take(10);
            string websiteRoot = Request.Url.GetLeftPart(UriPartial.Authority);//Full Website address with protocol
           
            var feed = new SyndicationFeed("Alaeddin Blog", "MyBlog RSS Feed",
                    new Uri(websiteRoot+"//home//feed"),
                    Guid.NewGuid().ToString(),
                    DateTime.Now);

            var items = new List<SyndicationItem>();

            //var d = Request.Url.OriginalString; ////@localhost:58025/home/feed
            //var x = Request.Url.PathAndQuery; ///home/feed
            //var dd = Request.Url.PathAndQuery;//home/feed
            //var dddd = Request.Url.LocalPath;
            //var ddd = Request.Url.AbsolutePath;
            //var ddwsd = Request.Url.AbsoluteUri;
           







            foreach (Post _post in posts)
            {
                string posturl = String.Format(@"/Post/Details/{0}", _post.PostId);
                
                string postlink = websiteRoot + posturl;
                var item = new SyndicationItem(_post.Title , _post.Post_Content,new Uri(postlink));
                items.Add(item);
            }
            feed.Items = items;
            return new RssActionResult { Feed = feed };
        }
}
}