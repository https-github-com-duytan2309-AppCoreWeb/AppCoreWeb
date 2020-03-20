using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Blog;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Models.BlogViewModels
{
    public class DetailBlogViewModel
    {
        public BlogViewModel Blog { get; set; }

        public bool Available { set; get; }

        public List<BlogViewModel> GetReatedBlogs { get; set; }

        public BlogCategoryViewModel BlogCategory { get; set; }

        //public List<ProductImageViewModel> ProductImages { set; get; }

        public List<BlogViewModel> UpsellBlog { get; set; }

        public List<BlogViewModel> LastestBlog { get; set; }

        public List<TagViewModel> Tags { set; get; }
    }
}
