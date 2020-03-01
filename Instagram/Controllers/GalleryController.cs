using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers
{
    public class GalleryController : Controller
    {
        private InstagramDbContext _context;

        public GalleryController()
        {
            _context = new InstagramDbContext(new DbContextOptions<InstagramDbContext>());
        }

        public IActionResult Index()
        {
            var travelsImageTag = new List<Tag>();
            var carImageTags = new List<Tag>();

            var tag1 = new Tag()
            {
                Id = 0,
                Title = "Camp"
            };
            var tag2 = new Tag()
            {
                Id = 1,
                Title = "BMW"
            };
            var tag3 = new Tag()
            {
                Id = 2,
                Title = "Porshe"
            };

            // _context.Tags.Add(tag1);
            // _context.Tags.Add(tag2);
            // _context.Tags.Add(tag3);


            travelsImageTag.Add(tag1);
            carImageTags.AddRange(new List<Tag> { tag2, tag3 });

            var postList = new List<Post>()
                {
                    new Post()
                    {
                        Title = "Create drim",
                        Url = "https://images.pexels.com/photos/1252500/pexels-photo-1252500.jpeg",
                        Created = DateTime.Now,
                        Likes = 228,
                        Dislikes = 0,
                        Tags = travelsImageTag
                    },

                    new Post()
                    {
                    Title = "Follow aim",
                    Url = "https://images.pexels.com/photos/167699/pexels-photo-167699.jpeg",
                    Created = DateTime.Now,
                    Likes = 238,
                    Dislikes = 0,
                    Tags = travelsImageTag
                    },

                    new Post()
                    {
                        Title = "Ta4ka prosto bomba",
                        Url = "https://images.pexels.com/photos/590798/pexels-photo-590798.jpeg",
                        Created = DateTime.Now,
                        Likes = 24,
                        Dislikes = 0,
                        Tags = carImageTags
                    }
                };
            //_context.Posts.AddRange(postList);
            //_context.SaveChanges();
            var model = new GalleryIndexModel()
            {
                Posts = postList
            };

            return View(model);
        }
    }
}