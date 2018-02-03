using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class CategoryForDisplay
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string ArticleType { get; set; }
    }
}
