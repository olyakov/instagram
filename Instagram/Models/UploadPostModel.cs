﻿using Microsoft.AspNetCore.Http;

namespace Instagram.Models
{
    public class UploadPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string Url { get; set; }
    }
}
