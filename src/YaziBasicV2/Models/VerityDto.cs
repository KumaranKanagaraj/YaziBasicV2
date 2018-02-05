using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class VerityDto
    {
        public Guid VerityId { get; set; }

        public string Title { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string ImagePath { get; set; }

        public string Tags { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        public Guid SocialImpressionId { get; set; }
    }
}
