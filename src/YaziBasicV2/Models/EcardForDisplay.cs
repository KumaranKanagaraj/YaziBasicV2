using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class EcardForDisplay
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string ImagePath { get; set; }

        public string Tags { get; set; }

        public bool IsPublic { get; set; }

        public string CreatedTime { get; set; }

        public string UpdatedTime { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public bool IsLiked { get; set; } = false;

        public int Likes { get; set; }

    }
}
