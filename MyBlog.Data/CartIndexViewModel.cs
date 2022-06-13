using System.Collections.Generic;
using System.Linq;

    namespace MyBlog.Data
    {
        public class CartIndexViewModel
        {
            public ShippingDetail ShippingDetail { get; set; }
            public Cart Cart { get; set; }
            public string ReturnUrl { get; set; }
        }
    }